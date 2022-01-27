using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Employees;
using WebStore.Domain.People;
using WebStore.Interfaces.Services;

namespace WebStoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;

        
        public EmployeesApiController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _EmployeesData.GetAll();
            return Ok(employees.ToDTO());
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();

            return Ok(employee.ToDTO());
        }

        [HttpGet]
        [Route("positions")]
        public IActionResult GetPos()
        {
            var positions = _EmployeesData.GetAllPositions();
            return Ok(positions.ToDTO());
        }

        [HttpGet("position/{Id}")]
        public IActionResult GetByIdPos(int Id)
        {
            var position = _EmployeesData.GetByIdPosition(Id);
            if (position is null)
                return NotFound();

            return Ok(position.ToDTO());
        }
        [HttpGet("position/{NamePosition}")]
        public IActionResult GetByNamePos(string NamePosition)
        {
            var position = _EmployeesData.GetByNamePosition(NamePosition);
            if (position is null)
                return NotFound();

            return Ok(position.ToDTO());
        }

        [HttpPost]
        public IActionResult Add(EmployeeDTO employee)
        {
            var id = _EmployeesData.Add(employee!.FromDTO()!);
            return CreatedAtAction(nameof(GetById), new { Id = id }, employee);
        }

        [HttpPut]
        public IActionResult Update(EmployeeDTO employee)
        {
            var success = _EmployeesData.Edit(employee!.FromDTO()!);
            return Ok(success);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = _EmployeesData.Delete(Id);
            return result
                ? Ok(true)
                : NotFound(false);
        }
 
    }
}
