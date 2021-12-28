using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Domain.Entities
{
    [Index(nameof(Name))]
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public Section Parent { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
