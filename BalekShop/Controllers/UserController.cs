using BalekShop.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BalekShop.Controllers
{
    public class UserController : Controller
    {

		private readonly IBookService bookService;
		private readonly IAuthorService authorService;
		private readonly IGenreService genreService;
		private readonly IPublisherService publisherService;
		public UserController(IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService)
		{
			this.bookService = bookService;
			this.genreService = genreService;
			this.publisherService = publisherService;
			this.authorService = authorService;
		}

		public IActionResult Store()
        {
			var data = bookService.GetAll();
			return View(data);
        }
    }
}
