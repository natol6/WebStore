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
            if(Filter?.Section.Name != null)
                query = query.Where(p => p.Section == Filter.Section);
            if (Filter?.Brand.Name != null)
                query = query.Where(p => p.Brand == Filter.Brand);
            return query;
        }
    }
}
