using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Employees;
using WebStore.Domain.People;
using WebStore.Domain.References;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(HttpClient Client) : base(Client, "api/employees")
        {
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = Get<IEnumerable<EmployeeDTO>>(Address);
            return employees!.FromDTO()!;
        }

        public Employee? GetById(int id)
        {
            var result = Get<EmployeeDTO>($"{Address}/{id}");
            return result.FromDTO();
        }

        public int Add(Employee employee)
        {
            var response = Post(Address, employee);
            var added_employee = response.Content.ReadFromJsonAsync<Employee>().Result;
            if (added_employee is null)
                return -1;
            var id = added_employee.Id;
            employee.Id = id;
            return id;
        }

        public bool Edit(Employee employee)
        {
            var response = Put(Address, employee);
            var success = response.EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<bool>()
               .Result;
            return success;
        }

        public bool Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var success = response.IsSuccessStatusCode;
            return success;
        }

        IEnumerable<PositionClass> IEmployeesData.GetAllPositions()
        {
            var positions = Get<IEnumerable<PositionClassDTO>>($"{Address}/positions");
            return positions!.FromDTO()!;
        }

        PositionClass? IEmployeesData.GetByIdPosition(int id)
        {
            var result = Get<PositionClassDTO>($"{Address}/position/{id}");
            return result.FromDTO();
        }

        PositionClass? IEmployeesData.GetByNamePosition(string name)
        {
            var result = Get<PositionClassDTO>($"{Address}/position/{name}");
            return result.FromDTO();
        }
    }
}
