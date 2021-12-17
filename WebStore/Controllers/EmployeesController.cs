using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> __Employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 28, Position = "Старший продавец", DateOfEmployment = new DateOnly(2019, 6, 15) },
            new Employee { Id = 2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 23, Position = "Главный бухгалтер", DateOfEmployment = new DateOnly(2018, 10, 28) },
            new Employee { Id = 3, LastName = "Баширов", FirstName = "Руслан", Patronymic = "Михайлович", Age = 32, Position = "Директор", DateOfEmployment = new DateOnly(2017, 3, 3) },
            new Employee { Id = 4, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18, Position = "Продавец", DateOfEmployment = new DateOnly(2019, 6, 15) },
        };

        public IActionResult Index()
        {
            var result = __Employees;
            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var employee = __Employees.FirstOrDefault(item => item.Id == id);
            if (employee == null)
                return NotFound();
            ViewBag.Image = String.Format("{0}.png", employee.Id);
            return View(employee);
        }
    }
}
