using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.Data;

namespace WebStore.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ICollection<Employee> _Employees;
        private int MaxFreeId;
        public InMemoryEmployeesData()
        {
            _Employees = TestData.Employees;
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
                return false;
            _Employees.Remove(employee);
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
                return false;
            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
            db_employee.Position = employee.Position;
            db_employee.DateOfEmployment = employee.DateOfEmployment;
            return true;
        }

        public IEnumerable<Employee> GetAll() => _Employees;
        

        public Employee? GetById(int id) => _Employees.FirstOrDefault(employee => employee.Id == id);
        
    }
}
