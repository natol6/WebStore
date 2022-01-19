using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        
        public ICollection<Product>? Products { get; set; }
    }
}
