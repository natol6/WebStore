using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;
using WebStore.Data;
using WebStore.Domain;

namespace WebStore.Services.InMemory
{
    [Obsolete("Используйте класс WebStore.Servises.InSQL.SqlProductData")]
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands()
        {
            return TestData.Brands;
        }

        public IEnumerable<Section> GetSections()
        {
            return TestData.Sections;
        }
        
        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IEnumerable<Product> query = TestData.Products;
            if (Filter?.SectionId != null)
                query = query.Where(p => p.SectionId == Filter.SectionId);
            if (Filter?.BrandId != null)
                query = query.Where(p => p.BrandId == Filter.BrandId);
            return query;
        }
    }
}
