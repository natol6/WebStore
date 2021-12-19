using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 28, Position = "Старший продавец", DateOfEmployment = new DateOnly(2019, 6, 15) },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 23, Position = "Главный бухгалтер", DateOfEmployment = new DateOnly(2018, 10, 28) },
            new Employee { Id = 3, LastName = "Баширов", FirstName = "Руслан", Patronymic = "Михайлович", Age = 32, Position = "Директор", DateOfEmployment = new DateOnly(2017, 3, 3) },
            new Employee { Id = 4, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18, Position = "Продавец", DateOfEmployment = new DateOnly(2019, 6, 15) },
        };
    }
}
