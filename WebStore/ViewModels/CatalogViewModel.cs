using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class CatalogViewModel
    {
        public Brand? Brand { get; set; }
        public Section? Section { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; } = null!;
    }
}
