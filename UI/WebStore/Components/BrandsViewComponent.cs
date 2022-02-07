using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{

    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public BrandsViewComponent(IProductData productData) => _ProductData = productData;
        public IViewComponentResult Invoke(string BrandId)
        {
            var brand_id = int.TryParse(BrandId, out var id) ? id : (int?)null;

            var brands = GetBrands();
            
            return View(new SelectableBrandsViewModel
            {
                BrandId = brand_id,
                Brands = brands,
            });
        }

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands()
            .OrderBy(b => b.Order)
            .Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
            });
        
    }
}
