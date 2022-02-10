﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }
        
        public IActionResult Index(int? brandId, int? sectionId, int Page = 1, int? PageSize = null) 
        {
            var page_size = PageSize
            ?? (int.TryParse(_Configuration["CatalogPageSize"], out var value) ? value : null);

            var filter = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = Page,
                PageSize = page_size,
            };
            var (products, total_count) = _ProductData.GetProducts(filter);

            var catalog_model = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.OrderBy(p => p.Order).ToView(),
                PageViewModel = new()
                {
                    Page = Page,
                    PageSize = page_size ?? 0,
                    TotalItems = total_count,
                },
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
