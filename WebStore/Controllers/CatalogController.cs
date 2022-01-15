using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Mapping;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        
        public CatalogController(IProductData ProductData) => _ProductData = ProductData;
        
        public IActionResult Index(int? brandId, int? sectionId) 
        {
            var filter = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
            };
            var products = _ProductData.GetProducts(filter);
            var catalog_model = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.OrderBy(p => p.Order).ToView(),

            };
            return View(catalog_model); 
        }
        public IActionResult Details(int Id)
        {
            var product = _ProductData.GetProductById(Id);
            //CultureInfo.CurrentUICulture = 
            //    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
            if (product is null)
                return NotFound();

            return View(product.ToView());
        }
    }
}
