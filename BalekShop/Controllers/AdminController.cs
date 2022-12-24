using BalekShop.Models;
using BalekShop.Repositories.Abstract;
using BalekShop.Repositories.Language;
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
                TempData["msg"] = "successful";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "error-server";
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
                return RedirectToAction("Get");
            }
            TempData["msg"] = "error-server";
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
            return RedirectToAction("Get");
        }

        public IActionResult Get()
        {
            var data = service.Get();
            return View(data);
        }
    }
}
