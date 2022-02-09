using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;


namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var products = ProductData.GetProducts().OrderBy(p => p.Order).Take(6).ToView();
            ViewBag.Products = products;
                        
            return View();
        }
        
        public IActionResult Error404() => View();
        
        public void Throw(string message) => throw new ApplicationException(message);

        public IActionResult Status(string Code)
        {
            switch (Code)
            {
                default:
                    return Content($"Status code - {Code}");

                case "404":
                    return RedirectToAction(nameof(Error404));
            }
        }

    }
}
