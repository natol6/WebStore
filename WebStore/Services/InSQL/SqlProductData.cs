using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;
using WebStore.Data;
using WebStore.Domain;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;
        
        public SqlProductData(WebStoreDB db) => _db = db;
        
        public IEnumerable<Brand> GetBrands() => _db.Brands;
        
        public IEnumerable<Section> GetSections() => _db.Sections;
        
        public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
        {
            IQueryable<Product> query = _db.Products;
            if (Filter?.SectionId != null)
                query = query.Where(p => p.SectionId == Filter.SectionId);
            if (Filter?.BrandId != null)
                query = query.Where(p => p.BrandId == Filter.BrandId);
            return query;
        }

        public Product? GetProductById(int Id) => _db.Products
       .Include(p => p.Brand)
       .Include(p => p.Section)
       .FirstOrDefault(p => p.Id == Id);
    }
}
