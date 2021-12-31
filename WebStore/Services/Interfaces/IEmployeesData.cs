using WebStore.Domain.People;
using WebStore.Domain.References;

namespace WebStore.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<PositionClass> GetAllPositions();
        Employee? GetById(int id);
        PositionClass? GetByIdPosition(int id);
        PositionClass? GetByNamePosition(string name);
        int Add(Employee employee);
        bool Edit(Employee employee);
        bool Delete(int id);
    }
}
