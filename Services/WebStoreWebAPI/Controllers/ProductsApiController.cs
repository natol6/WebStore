﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStoreWebAPI.Controllers
{
    [ApiController]
    [Route(WebAPIAddresses.Products)]
    public class ProductsApiController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")] 
        public IActionResult GetSections()
        {
            var sections = _ProductData.GetSections();
            return Ok(sections.ToDTO());
        }

        [HttpGet("sections/{Id}")] 
        public IActionResult GetSectionById(int Id)
        {
            var section = _ProductData.GetSectionById(Id);
            if (section is null)
                return NotFound();

            return Ok(section.ToDTO());
        }

        [HttpGet("brands")] // GET -> http://localhost:5001/api/products/brands
        public IActionResult GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return Ok(brands.ToDTO());
        }

        [HttpGet("brands/{Id}")] // GET -> http://localhost:5001/api/products/brands/5
        public IActionResult GetBrandById(int Id)
        {
            var brand = _ProductData.GetBrandById(Id);
            if (brand is null)
                return NotFound();

            return Ok(brand.ToDTO());
        }

        [HttpPost]
        public IActionResult GetProducts(ProductFilter? Filter = null)
        {
            var products = _ProductData.GetProducts(Filter);
            return Ok(products.ToDTO());
        }

        [HttpGet("{Id}")]
        public IActionResult GetProductById(int Id)
        {
            var product = _ProductData.GetSectionById(Id);
            if (product is null)
                return NotFound();

            return Ok(product.ToDTO());
        }

        [HttpPost("new/{Name}")]
        public IActionResult CreateProduct(CreateProductDTO Model)
        {
            var product = _ProductData.CreateProduct(Model.Name, Model.Order, Model.Price, Model.ImageUrl, Model.Section, Model.Brand);
            return CreatedAtAction(nameof(GetProductById), new { product.Id }, product.ToDTO());
        }
    }
}
