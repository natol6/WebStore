using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? Patronymic { get; set; }

        public int Age { get; set; }

        public PositionClassDTO Position { get; set; } = null!;

        public DateTime DateOfEmployment { get; set; }
    }
}
