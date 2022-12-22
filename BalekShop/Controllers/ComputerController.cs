using Microsoft.AspNetCore.Mvc;

namespace BalekShop.Controllers
{
    public class ComputerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
