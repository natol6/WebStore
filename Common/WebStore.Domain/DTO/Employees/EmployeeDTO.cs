using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO.Employees
{
    /// <summary>Сотрудник</summary>
    public class EmployeeDTO
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Фамилия</summary>
        public string LastName { get; set; } = null!;

        /// <summary>Имя</summary>
        public string FirstName { get; set; } = null!;

        /// <summary>Отчество</summary>
        public string? Patronymic { get; set; }

        /// <summary>Возраст</summary>
        public int Age { get; set; }

        /// <summary>Должность</summary>
        public PositionClassDTO Position { get; set; } = null!;

        /// <summary>Дата приема на работу</summary>
        public DateTime DateOfEmployment { get; set; }
    }
}
