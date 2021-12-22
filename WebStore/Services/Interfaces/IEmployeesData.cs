using WebStore.ViewModels;

namespace WebStore.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<EmployeeViewModel> GetAll();
        EmployeeViewModel? GetById(int id);
        int Add(EmployeeViewModel employee);
        bool Edit(EmployeeViewModel employee);
        bool Delete(int id);
    }
}
