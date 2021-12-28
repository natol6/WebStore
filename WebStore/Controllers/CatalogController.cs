using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        public CatalogController(IProductData ProductData) => _ProductData = ProductData;
        
        public IActionResult Index(Brand? brand, Section? section) 
        {
            var filter = new ProductFilter
            {
                Brand = brand,
                Section = section,
            };
            var products = _ProductData.GetProducts(filter);
            var catalog_model = new CatalogViewModel
            {
                Brand = brand,
                Section = section,
                Products = products.OrderBy(p => p.Order).Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                }),

            };
            return View(catalog_model); 
        }
    }
}
