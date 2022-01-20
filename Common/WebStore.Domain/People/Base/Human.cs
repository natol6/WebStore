using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.People.Base
{
    public abstract class Human : Entity
    {
        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;
        
        public string? Patronymic { get; set; }
    }
}
