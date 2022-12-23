using BalekShop.Models.Domain;
using BalekShop.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(User user)
		{

			return View();
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

		[HttpGet]
		public IActionResult Signup()
		{
            TempData["msg"] = "Get";
            return View();
		}
	}
}
