using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Employees;
using WebStore.Domain.People;
using WebStore.Domain.References;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStoreWebAPI.Controllers
{
    [ApiController]
    [Route(WebAPIAddresses.Employees)]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IEmployeesData _EmployeesData;

        
        public EmployeesApiController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        ///<summary>Получение всех сотрудников</summary>
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _EmployeesData.GetAll();
            return Ok(employees.ToDTO());
        }

        /// <summary>Получение сотрудника по его идентификатору</summary>
        /// <param name="Id">Идентификатор сотрудника</param>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int Id)
        {
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();

            return Ok(employee.ToDTO());
        }

        /// <summary>Получение перечня всех должностей работников магазина</summary>
        [HttpGet]
        [Route("positions")]
        public IActionResult GetPos()
        {
            var positions = _EmployeesData.GetAllPositions();
            return Ok(positions.ToDTO());
        }

        /// <summary>Получение должности из перечня должностей магазина по ее идентификатору</summary>
        /// <param name="Id">Идентификатор должности</param>
        [HttpGet("position/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PositionClassDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByIdPos(int Id)
        {
            var position = _EmployeesData.GetByIdPosition(Id);
            if (position is null)
                return NotFound();

            return Ok(position.ToDTO());
        }

        /// <summary>Получение должности из перечня должностей магазина по ее наименованию</summary>
        /// <param name="NamePosition">наименование должности</param>
        [HttpGet("position/{NamePosition}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PositionClassDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByNamePos(string NamePosition)
        {
            var position = _EmployeesData.GetByNamePosition(NamePosition);
            if (position is null)
                return NotFound();

            return Ok(position.ToDTO());
        }

        /// <summary>Добавление нового сотрудника</summary>
        /// <param name="employee">Новый сотрудник</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeDTO))]
        public IActionResult Add(EmployeeDTO employee)
        {
            var id = _EmployeesData.Add(employee!.FromDTO()!);
            return CreatedAtAction(nameof(GetById), new { Id = id }, employee);
        }

        /// <summary>Обновление информации о сотруднике</summary>
        /// <param name="employee">Структура с информацией о сотруднике</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public IActionResult Update(EmployeeDTO employee)
        {
            var success = _EmployeesData.Edit(employee!.FromDTO()!);
            return Ok(success);
        }

        /// <summary>Удаление сотрудника по его идентификатору</summary>
        /// <param name="Id">Идентификатор удаляемого сотрудника</param>
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public IActionResult Delete(int Id)
        {
            var result = _EmployeesData.Delete(Id);
            return result
                ? Ok(true)
                : NotFound(false);
        }
 
    }
}
