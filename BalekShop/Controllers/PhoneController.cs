using BalekShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace BalekShop.Controllers
{
    public class PhoneController : Controller
    {
        public IActionResult Index()
        {
            List<ProductModel> products = new List<ProductModel>();
            ProductModel newP = new ProductModel();
            newP.Id = 1;
            newP.Price = 40000;
            newP.Name = "Iphone 14";
            newP.ImageSource = "https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mq0g3tua-apple-iphone-14-pro-128gb-derin-mor-mq0g3tua-637987545722692659.jpg";
            newP.Description = "This is Apple device";

            ProductModel newP2 = new ProductModel();
            newP2.Id = 2;
            newP2.Price = 38000;
            newP2.Name = "Samsung Galaxy S22";
            newP2.ImageSource = "https://cdn.cimri.io/image/1000x1000/samsunggalaxysultraggbgbraminmpakllceptelefonubordo_580363933.jpg";
            newP2.Description = "This is Samsung device";

            products.Add(newP);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);
            products.Add(newP2);

            return View(products);
        }
    }
}
