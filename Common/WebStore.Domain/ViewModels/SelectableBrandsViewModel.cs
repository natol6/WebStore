using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class SelectableBrandsViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? BrandId { get; set; }

    }
}
