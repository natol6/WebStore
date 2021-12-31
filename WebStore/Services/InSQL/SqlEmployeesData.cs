using WebStore.Domain.People;
using WebStore.Services.Interfaces;
using WebStore.Data;
using WebStore.DAL.Context;
using WebStore.Domain.References;

namespace WebStore.Services.InSQL
{
    public class SqlEmployeesData : IEmployeesData
    {
        private readonly ILogger<SqlEmployeesData> _Logger;
        private readonly WebStoreDB _db;
        public SqlEmployeesData(WebStoreDB db, ILogger<SqlEmployeesData> logger)
        {
            _Logger = logger;
            _db = db;
        }
        public int Add(Employee employee)
        {
            if (employee is null)
            {
                _Logger.LogInformation("Попытка добавить сотрудника с незаполненными данными");
                throw new ArgumentNullException(nameof(employee));
            }

            if (_db.Employees.Contains(employee))
            {
                _Logger.LogInformation("Попытка добавить существующего сотрудника Id: {0}", employee.Id);
                return employee.Id;
            }
            _db.Employees.Add(employee);
            _db.SaveChanges();
            _Logger.LogInformation("Сотрудник Id: {0} успешно добавлен", employee.Id);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if(employee is null)
            {
                _Logger.LogWarning("Попытка удаления отсутствующего сотрудника с Id: {0}", id);
                return false;
            }
                
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            _Logger.LogInformation("Сотрудник Id: {0} был удален", employee.Id);
            return true;
        }

        public bool Edit(Employee employee)
        {
            _db.Employees.Update(employee);
            var result = _db.SaveChanges() != 0;
            if (result)
                _Logger.LogInformation("Информация о сотруднике Id: {0} была изменена", employee.Id);
            return result;
        }

        public IEnumerable<Employee> GetAll() => _db.Employees.AsEnumerable();
        

        public Employee? GetById(int id) => _db.Employees.Find(id);
        public IEnumerable<PositionClass> GetAllPositions() => _db.Positions.AsEnumerable();
        public PositionClass? GetByIdPosition(int id) => _db.Positions.FirstOrDefault(p => p.Id == id);
        public PositionClass? GetByNamePosition(string name) => _db.Positions.FirstOrDefault(p => p.Name == name);
    }
}
