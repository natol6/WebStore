using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Domain
{
    public class ProductFilter
    {
        public Section? Section { get; set; }
        public Brand? Brand { get; set; }
    }
}
