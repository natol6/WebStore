using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BreadCrumbsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke()
        {
            var model = new BreadCrumbsViewModel();

            
            if (int.TryParse(Request.Query["SectionId"], out var section_id))
            {
                model.Section = _ProductData.GetSectionById(section_id);
                if (model.Section?.ParentId is { } parent_section_id && model.Section.Parent is null)
                    model.Section.Parent = _ProductData.GetSectionById(parent_section_id)!;
            }

            if (int.TryParse(Request.Query["BrandId"], out var brand_id))
                model.Brand = _ProductData.GetBrandById(brand_id);

            if (int.TryParse(Request.RouteValues["id"]?.ToString(), out var product_id))
            {
                var SelectProduct = _ProductData.GetProductById(product_id);
                model.Product = SelectProduct?.Name;
                model.Brand = SelectProduct?.BrandId is not null ? _ProductData.GetBrandById((int)SelectProduct.BrandId) : null;
                model.Section = _ProductData.GetSectionById(SelectProduct.SectionId);
                if (model.Section?.ParentId is { } parent_section_id && model.Section.Parent is null)
                    model.Section.Parent = _ProductData.GetSectionById(parent_section_id)!;
            }
                

            return View(model);
        }
    }
}
