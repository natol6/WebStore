using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 28, Position = "Старший продавец", DateOfEmployment = "15.06.2019" },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 23, Position = "Главный бухгалтер", DateOfEmployment = "28.10.2018" },
            new Employee { Id = 3, LastName = "Баширов", FirstName = "Руслан", Patronymic = "Михайлович", Age = 32, Position = "Директор", DateOfEmployment = "03.03.2017" },
            new Employee { Id = 4, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18, Position = "Продавец", DateOfEmployment = "25.11.2020" },
        };
    }
}
