using WebStore.Domain.People;
using WebStore.Services.Interfaces;
using WebStore.Data;
using WebStore.Domain.References;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> _Logger;
        private readonly ICollection<Employee> _Employees;
        private readonly ICollection<PositionClass> _Positions;
        private int MaxFreeId;
        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> logger)
        {
            _Logger = logger;
            _Employees = TestData.Employees;
            _Positions = TestData.Positions;
            MaxFreeId = _Employees.DefaultIfEmpty().Max(e => e?.Id ?? 0) + 1;
        }
        public int Add(Employee employee)
        {
            if(employee is null)
                throw new ArgumentNullException(nameof(employee));
            if (_Employees.Contains(employee))
                return employee.Id;
            
            employee.Id = MaxFreeId++;
            _Employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if(employee is null)
            {
                _Logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id: {0}", employee.Id);
                return false;
            }
                
            _Employees.Remove(employee);
            _Logger.LogInformation("Сотрудник Id: {0} был удален", employee.Id);
            return true;
        }

        public bool Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));
            if (_Employees.Contains(employee))
                return true;
            var db_employee = GetById(employee.Id);
            if(db_employee is null)
            {
                _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id: {0}", employee.Id);
                return false;
            }
                
            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
            db_employee.Position = employee.Position;
            db_employee.DateOfEmployment = employee.DateOfEmployment;
            _Logger.LogInformation("Информация о сотруднике Id: {0} была изменена", employee.Id);
            return true;
        }

        public IEnumerable<Employee> GetAll() => _Employees;

        public IEnumerable<PositionClass> GetAllPositions() => _Positions;
        public Employee? GetById(int id) => _Employees.FirstOrDefault(employee => employee.Id == id);
        public PositionClass? GetByIdPosition(int id) => _Positions.FirstOrDefault(p => p.Id == id);
        public PositionClass? GetByNamePosition(string name) => _Positions.FirstOrDefault(p => p.Name == name);
    }
}
