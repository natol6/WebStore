using WebStore.Domain.DTO.Employees;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.People;

namespace WebStore.Domain.DTO.Employees
{
    public static class EmployeeDTOMapper
    {
        public static EmployeeDTO? ToDTO(this Employee? empl) => empl is null
            ? null
            : new EmployeeDTO
            {
                Id = empl.Id,
                LastName = empl.LastName,
                FirstName = empl.FirstName,
                Patronymic = empl.Patronymic,
                Age = empl.Age,
                Position = empl.Position.ToDTO()!,
                DateOfEmployment = empl.DateOfEmployment,
            };

        public static Employee? FromDTO(this EmployeeDTO? empl) => empl is null
            ? null
            : new Employee
            {
                Id = empl.Id,
                LastName = empl.LastName,
                FirstName = empl.FirstName,
                Patronymic = empl.Patronymic,
                Age = empl.Age,
                PositionId = empl.Position.Id,
                Position = empl.Position.FromDTO()!,
                DateOfEmployment = empl.DateOfEmployment,
            };

        public static IEnumerable<EmployeeDTO?> ToDTO(this IEnumerable<Employee?> employees) => employees.Select(ToDTO);

        public static IEnumerable<Employee?> FromDTO(this IEnumerable<EmployeeDTO?> employees) => employees.Select(FromDTO);
    }
}
