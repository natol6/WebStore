using WebStore.Domain.References;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace WebStore.Services.Services.InSQL
{
    public class SqlPositionsData : IPositionsData
    {
        private readonly ILogger<SqlPositionsData> _Logger;

        private readonly WebStoreDB _db;

        public SqlPositionsData(WebStoreDB db, ILogger<SqlPositionsData> logger)
        {
            _Logger = logger;
            _db = db;
        }

        public int Add(PositionClass position)
        {
            if (position is null)
            {
                _Logger.LogInformation("Попытка добавить должность с незаполненными данными");
                throw new ArgumentNullException(nameof(position));
            }

            if (_db.Positions.Contains(position))
            {
                _Logger.LogInformation("Попытка добавить существующую должность Id: {0}", position.Id);
                return position.Id;
            }


            _db.Positions.Add(position);
            _db.SaveChanges();
            _Logger.LogInformation("Должность Id: {0} успешно добавлена", position.Id);
            return position.Id;
        }

        public bool Delete(int id)
        {
            var position = GetById(id);
            if (position is null)
            {
                _Logger.LogWarning("Попытка удаления отсутствующей должности сотрудника с Id: {0}", id);
                return false;
            }

            _db.Positions.Remove(position);
            _db.SaveChanges();
            _Logger.LogInformation("Должность сотрудника Id: {0} была удалена", position.Id);
            return true;
        }

        public bool Edit(PositionClass position)
        {
            _db.Positions.Update(position);
            var result = _db.SaveChanges() != 0;
            if (result)
                _Logger.LogInformation("Информация о должности сотрудника Id: {0} была изменена", position.Id);
            return result;
        }

        public IEnumerable<PositionClass> GetAll() => _db.Positions.Include(p => p.Employees).AsEnumerable();

        public PositionClass? GetById(int id) => _db.Positions.Include(p => p.Employees).FirstOrDefault(position => position.Id == id);

        //public PositionClass? GetByName(string name) => _db.Positions.FirstOrDefault(position => position.Name == name);
        //public string GetName(int id) => _db.Positions.FirstOrDefault(p => p.Id == id).Name;
    }
}
