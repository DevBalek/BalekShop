using BalekShop.Models.Domain;
using BalekShop.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BalekShop.Controllers
{
	public class AdminController : Controller
	{
        private readonly IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Admin model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

		
		public IActionResult Add()
		{
			return View();
		}

        [HttpPost]
        public IActionResult Update(Admin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }
        public IActionResult Delete(int id)
        {

            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }
    }
}
