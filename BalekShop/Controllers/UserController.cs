using BalekShop.Models;
using BalekShop.Repositories.Abstract;
using BalekShop.Repositories.Language;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Security.Claims;

namespace BalekShop.Controllers
{

    public class UserController : Controller
    {

		private readonly IBookService bookService;
		private readonly IAuthorService authorService;
		private readonly IGenreService genreService;
		private readonly IPublisherService publisherService;
		private readonly IUserService userService;
		private readonly ICartService cartService;
        private readonly IAdminService adminService;
        private IHttpContextAccessor httpContextAccessor;

		public UserController(IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService,IUserService userService,ICartService cartService, IAdminService adminService, IHttpContextAccessor httpContextAccessor)
		{
			this.bookService = bookService;
			this.genreService = genreService;
			this.publisherService = publisherService;
			this.authorService = authorService;
			this.userService = userService;
			this.cartService = cartService;
			this.httpContextAccessor = httpContextAccessor;
            this.adminService = adminService;
			
			
		}

		[HttpGet]
		public IActionResult Store()
        {
			dynamic user;
			string? username = null;
			try
			{
				user = httpContextAccessor.HttpContext.User;
				username= user.FindFirst(ClaimTypes.Name).Value;
			}
			catch
			{

			}

			ViewBag.username = username;

			var data = bookService.Get();			

			return View(data);
        }

		[Authorize]
		public IActionResult Cart()
		{
			dynamic user;
			string userIdString = "";
			List<Cart>? myCart = null;

			List<BookCart> bookCarts = new List<BookCart>();

			try
			{
				user = httpContextAccessor.HttpContext.User;
				userIdString = user.FindFirst(ClaimTypes.NameIdentifier).Value;

				int userID = Convert.ToInt32(userIdString);

				myCart = cartService.Get().Where(a => a.UserID == userID).ToList();
				
				foreach(var item in myCart)
				{
					bookCarts.Add( new BookCart { Amount= Convert.ToInt32(item.Amount),Book=bookService.FindById(Convert.ToInt32(item.BookID)) });
				}

			}
			catch
			{
			}

			return View(bookCarts);
		}

		[Authorize]
        public IActionResult AddCart(int id)
		{
			dynamic user;
			string userIdString = "";
			List<Cart>? myCart=null;

			try {
				user = httpContextAccessor.HttpContext.User;
				userIdString = user.FindFirst(ClaimTypes.NameIdentifier).Value;
				
				int userID = Convert.ToInt32(userIdString);

				myCart = cartService.Get().Where(a=>a.UserID==userID).ToList();

				if (myCart.Count == 0)
				{
					TempData["msg"] = "first-item";
					cartService.Add(new Cart { UserID = userID, Amount = 1,BookID = id });
				}
				else
				{
					var isBookExist = myCart!.Any(item => item.BookID == id);

					if (isBookExist)
					{
						TempData["msg"] = "a-lot-item";
						Cart cart = myCart!.Where(a => a.BookID == id).First();
						cart.Amount = cart.Amount + 1;
						cartService.Update(cart);

					}
					else
					{
						TempData["msg"] = "other-item";
						cartService.Add(new Cart { UserID = userID, Amount = 1, BookID = id });
					}
				}
								
			}
			catch
			{

			}
			

			//TempData["msg"] = "UserID: " + myCart?[0].UserID +"Cart Books: "+ ", Item id: "+id;
			return RedirectToAction(nameof(Store));
		}


        public IActionResult Login()
		{            
            return View();
		}

		
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["msg"] = "logout-success";

			return RedirectToAction(nameof(Store));
		}

		[HttpPost]
		public async Task<IActionResult> Login(ValidateUser model)
		{
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "Unvalid attempt";
                return View(model);
            }
			User? result = null;
            try
			{
				result = userService.Get().Where(a => a.Email == model.Email).Where(b => b.Password == model.Password).First();
			}
			catch {

				if (await AdminLoginAttempt(model))
				{
                    TempData["msg"] = "welcome";
                    return RedirectToAction(nameof(Store));
                }
				else
				{
                    TempData["msg"] = "wrong-email-or-pass";
                    return View(model);
                }
                
            }
            if (result != null)
            {
                TempData["msg"] = "welcome";
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, result.UserName),
					new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
					new Claim(ClaimTypes.Role, "User")
                };
				var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
				var authProperties = new AuthenticationProperties();


				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                
				return RedirectToAction(nameof(Store));
            }

            TempData["msg"] = "error-server";
			return View(model);
        }

		[HttpPost]
		public IActionResult Signup(User model)
		{            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = userService.Add(model);
            if (result)
            {
                TempData["msg"] = "signup-success";
				return RedirectToAction(nameof(Login));
            }
            TempData["msg"] = "already-using";
			return View(model);
        }

		public IActionResult Signup()
		{         
            return View();
		}

		public async Task<bool> AdminLoginAttempt(ValidateUser model)
		{
			Admin? result = null;
            try
            {
                result = adminService.Get().Where(a => a.Email == model.Email).Where(b => b.Password == model.Password).First();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Email),
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

				return true;
            }
            catch
            {
				return false;
            }			
		}

		[Authorize(Roles ="User")]
		public IActionResult Account()
		{
			dynamic user;
			string userIdString = "";

			try
			{
				user = httpContextAccessor.HttpContext.User;
				userIdString = user.FindFirst(ClaimTypes.NameIdentifier).Value;

				int userID = Convert.ToInt32(userIdString);
				
				var record = userService.FindById(userID);
				return View(record);
			}
			catch
			{
				return View();
			}
									
		}

		[HttpPost]
		public IActionResult Account(User model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = userService.Update(model);
			if (result)
			{
				return RedirectToAction(nameof(Store));
			}
			TempData["msg"] = "error-server";
			return View(model);
		}


		public IActionResult DeleteBook(int id)
		{
            dynamic user;
            string userIdString = "";
            List<Cart>? myCart = null;

            try
            {
                user = httpContextAccessor.HttpContext.User;
                userIdString = user.FindFirst(ClaimTypes.NameIdentifier).Value;

                int userID = Convert.ToInt32(userIdString);

                myCart = cartService.Get().Where(a => a.UserID == userID).ToList();

                if (myCart.Count != 0)                
                {
                    var isBookExist = myCart!.Any(item => item.BookID == id);

                    if (isBookExist)
                    {
                        TempData["msg"] = "successful";						
                        Cart cart = myCart!.Where(a => a.BookID == id).First();
						cartService.Delete(cart.Id);                  
                    }
                    else
                    {
                        TempData["msg"] = "error-server";                        
                    }
                }
            }
            catch
            {
				TempData["msg"] = "error-server";
			}
            
            return RedirectToAction(nameof(Cart));
        }


		//
		//
		//
		//
		//! USER ADMIN LEVEL
		//
		//
		//
		//

		[Authorize(Roles ="Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = userService.Add(model);
            if (result)
            {
                TempData["msg"] = "success";
				return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "error-server";
			return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var record = userService.FindById(id);
            return View(record);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = userService.Update(model);
            if (result)
            {
                return RedirectToAction("Get");
            }
            TempData["msg"] = "error-server";
			return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {

            var result = userService.Delete(id);
            return RedirectToAction("Get");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var data = userService.Get();
            return View(data);
        }

    }
}
