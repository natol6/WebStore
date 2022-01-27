using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO.Employees
{
    public class CreateEmployeeDTO
    {
        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? Patronymic { get; set; }

        public int Age { get; set; }

        public string Position { get; set; } = null!;

        public DateTime DateOfEmployment { get; set; }
    }
}
