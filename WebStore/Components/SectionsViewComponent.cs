using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Services.Interfaces;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData productData) => _ProductData = productData;
        public IViewComponentResult Invoke() => View();
        
    }
}
