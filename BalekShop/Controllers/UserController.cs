using BalekShop.Models.Domain;
using BalekShop.Repositories.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		public UserController(IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService,IUserService userService,ICartService cartService)
		{
			this.bookService = bookService;
			this.genreService = genreService;
			this.publisherService = publisherService;
			this.authorService = authorService;
			this.userService = userService;
			this.cartService = cartService;
		}

		[HttpGet]
		public IActionResult Store()
        {
			var data = bookService.GetAll();
			return View(data);
        }

		public IActionResult Cart()
		{
			return View();
		}

		public IActionResult Login()
		{            
            return View();
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
				result = userService.GetAll().Where(a => a.Email == model.Email).Where(b => b.Password == model.Password).First();
			}
			catch {
                TempData["msg"] = "Wrong Email or Password";
                return View(model);
            }
            if (result != null)
            {
                TempData["msg"] = "Welcome " +result.UserName + ",\nLogin Successfully";
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, result.UserName),
					new Claim(ClaimTypes.Role, "User")
                };
				var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
				var authProperties = new AuthenticationProperties();


				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                
				return RedirectToAction(nameof(Login));
            }

            TempData["msg"] = "Error has occured on server side,\nResult: " + result;
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
                TempData["msg"] = "Signup Successfully";
                return RedirectToAction(nameof(Login));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

		public IActionResult Signup()
		{         
            return View();
		}
	}
}
