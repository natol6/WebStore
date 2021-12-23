using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;
using WebStore.Data;


namespace WebStore.Services
{
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
    }
}
