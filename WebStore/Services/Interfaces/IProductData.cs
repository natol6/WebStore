using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IEnumerable<Product> query = TestData.Products;
            if(Filter?.SectionId != null)
                query = query.Where(p => p.SectionId == Filter.SectionId);
            if (Filter?.BrandId != null)
                query = query.Where(p => p.BrandId == Filter.BrandId);
            return query;
        }
    }
}
