using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;


namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index([FromServices]IProductData productData)
        {
            var products = productData
                .GetProducts()
                .OrderBy(p => p.Order)
                .Take(6)
                .ToView();
            
            ViewBag.Products = products;
            return View();
        }
        
        public IActionResult Error404() => View();
        
        public void Throw(string message) => throw new ApplicationException(message);
        
    }
}
