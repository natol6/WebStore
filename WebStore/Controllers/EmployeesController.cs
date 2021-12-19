using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Data;

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
    }
}
