using BalekShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalekShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public IActionResult Index()
        {           

            return View();
        }
    }
}
