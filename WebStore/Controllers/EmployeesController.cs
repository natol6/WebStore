using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Data;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly ICollection<Employee> __Employees;
        public EmployeesController()
        {
            __Employees = TestData.Employees;
        }
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
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var employee = __Employees.FirstOrDefault(item => item.Id == id);
            if (employee == null)
                return NotFound();

            var model = new EmployeeEditViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Position = employee.Position,
                DateOfEmployment = employee.DateOfEmployment,
            };
            return View(model);
        }
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // Обработка модели...
            
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) 
        { 
            return View(); 
        }
    }
}
