using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.People.Base
{
    public abstract class Human : Entity
    {
        public string LastName { get; set; }
        
        public string FirstName { get; set; }
        
        public string? Patronymic { get; set; }
    }
}
