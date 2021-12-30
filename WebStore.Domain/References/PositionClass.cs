using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.References
{
    [Index(nameof(Name), IsUnique = true)]
    public class PositionClass : NamedEntity 
    {
        
    }
}
