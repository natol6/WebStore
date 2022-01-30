using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO.Employees
{
    /// <summary>Должность сотрудника</summary>
    public class PositionClassDTO
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Наименование должности</summary>
        public string Name { get; set; } = null!;
    }
}
