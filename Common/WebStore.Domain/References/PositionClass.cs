using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.People;

namespace WebStore.Domain.References
{
    [Index(nameof(Name), IsUnique = true)]
    public class PositionClass : NamedEntity 
    {
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
