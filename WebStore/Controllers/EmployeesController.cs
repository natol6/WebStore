using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.People;
using WebStore.Domain.References;
using WebStore.Data;
using WebStore.ViewModels;
using WebStore.Services;
using WebStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStore.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly ILogger<EmployeesController> _Logger;
        private readonly IEmployeesData _EmployeesData;
        
        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> logger)
        {
            _Logger = logger;
            _EmployeesData = EmployeesData;
        }
        
        public IActionResult Index()
        {
            var employees = _EmployeesData.GetAll();
            //var positions = _EmployeesData.GetAllPositions().ToDictionary(p => p.Id);
            var employees_view = new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                
                var employee_view = new EmployeeViewModel
                    {
                    Id = employee.Id,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age,
                    Position = employee.Position.Name,
                    DateOfEmployment = employee.DateOfEmployment,
                    };
                employees_view.Add(employee_view);
            }
            return View(employees_view);
        }
        
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee == null)
                return NotFound();
            //var position = _EmployeesData.GetByIdPosition(employee.PositionId);
            ViewBag.Image = $"{employee.Id}.png";
            var model = new EmployeeViewModel
                 {
                    Id = employee.Id,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age,
                    Position = employee.Position.Name,
                    DateOfEmployment = employee.DateOfEmployment,
                };
            return View(model);
        }
        
        public IActionResult Create()
        {
            ViewBag.Positions = GetPositionsView();
            return View("Edit", new EmployeeViewModel());
        }
        
        public IActionResult Edit(int? id)
        {
            ViewBag.Positions = GetPositionsView();
            if (id == null)
                return View(new EmployeeViewModel());
            
            var employee = _EmployeesData.GetById((int)id);
            if (employee == null)
            {
                _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id: {0}", id);
                return NotFound();
            }
            //var position = _EmployeesData.GetByIdPosition(employee.PositionId);

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Position = employee.Position.Name,
                DateOfEmployment = employee.DateOfEmployment,
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            ViewBag.Positions = GetPositionsView();
            if (!ModelState.IsValid)
                return View(model);
            //var position = _EmployeesData.GetByNamePosition(model.Position);
            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age,
                PositionId = ViewBag.Positions.SelectedValue,
                DateOfEmployment = model.DateOfEmployment,
            };
            if(model.Id == 0)
            {
                 var id = _EmployeesData.Add(employee);
                _Logger.LogInformation("Создан новый сотрудник {0} ", id);
            }
                
            else if (!_EmployeesData.Edit(employee))
            {
                _Logger.LogInformation("Неуспешная попытка изменения информации о сотруднике {0} ", employee);
                return NotFound();
            }
                
            _Logger.LogInformation("Изменена информация о сотруднике {0} ", employee);
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id) 
        { 
            if(id < 0)
                return BadRequest();
            var employee = _EmployeesData.GetById(id);
            if (employee == null)
                return NotFound();
            //var position = _EmployeesData.GetByIdPosition(employee.PositionId);
            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Position = employee.Position.Name,
                DateOfEmployment = employee.DateOfEmployment,
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
            {
                _Logger.LogInformation("Неуспешная попытка удаления сотрудника Id: {0} ", id);
                return NotFound();
            }

            _Logger.LogInformation("Удален сотрудник Id: {0} ", id);
            return RedirectToAction("Index");
        }
        
        private SelectList GetPositionsView()
        {
            var positions = _EmployeesData.GetAllPositions();
            
            return new SelectList(positions, "Id", "Name");
        }

    }
}
