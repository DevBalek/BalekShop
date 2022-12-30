using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BalekShop.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using BalekShop.Models;
using BalekShop.Repositories.Language;

namespace BalekShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BookController : Controller
    {
        private readonly IAuthorService authorService;
		private readonly IPublisherService publisherService;
		private readonly IGenreService genreService;
		private readonly IBookService bookService;

		public BookController(IBookService bookService, IGenreService genreService, IPublisherService publisherService,IAuthorService authorService)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;
        }
        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = authorService.Get().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
            model.PublisherList = publisherService.Get().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
            model.GenreList = genreService.Get().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Book model)
        {
            model.AuthorList = authorService.Get().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(),Selected=a.Id==model.AuthorId}).ToList();
            model.PublisherList = publisherService.Get().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(),Selected=a.Id==model.PubhlisherId }).ToList();
            model.GenreList = genreService.Get().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(),Selected=a.Id==model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "successful";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "error-server";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var model = bookService.FindById(id);
			model.AuthorList = authorService.Get().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
			model.PublisherList = publisherService.Get().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
			model.GenreList = genreService.Get().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = authorService.Get().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.PublisherList = publisherService.Get().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PubhlisherId }).ToList();
            model.GenreList = genreService.Get().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Update(model);
            if (result)
            {
                return RedirectToAction(nameof(Get));
            }
            TempData["msg"] = "error-server";
            return View(model);
        }


        public IActionResult Delete(int id)
        {

            var result = bookService.Delete(id);
            return RedirectToAction(nameof(Get));
        }

        public IActionResult Get()
        {

            var data = bookService.Get();
            return View(data);
        }
    }
}
