﻿using Microsoft.AspNetCore.Mvc;
using BalekShop.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using BalekShop.Models;
using BalekShop.Repositories.Language;

namespace BalekShop.Controllers
{
    [Authorize(Roles = "Admin")]
	public class GenreController : Controller
    {
        private readonly IGenreService service;
  
        public GenreController(IGenreService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
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


        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction(nameof(Get));
            }
            TempData["msg"] = "error-server";
            return View(model);
        }


        public IActionResult Delete(int id)
        {

            var result = service.Delete(id);
            return RedirectToAction(nameof(Get));
        }

        public IActionResult Get()
        {

            var data = service.Get();
            return View(data);
        }

    }
}
