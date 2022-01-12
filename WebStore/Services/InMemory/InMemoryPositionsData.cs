using WebStore.Domain.References;
using WebStore.Services.Interfaces;
using WebStore.Data;

namespace WebStore.Services.InMemory
{
    [Obsolete("Используйте класс WebStore.Servises.InSQL.SqlPositionsData")]
    public class InMemoryPositionsData : IPositionsData
    {
        private readonly ILogger<InMemoryPositionsData> _Logger;
        private readonly ICollection<PositionClass> _Positions;
        private int MaxFreeId;
        
        public InMemoryPositionsData(ILogger<InMemoryPositionsData> logger)
        {
            _Logger = logger;
            _Positions = TestData.Positions;
            MaxFreeId = _Positions.DefaultIfEmpty().Max(e => e?.Id ?? 0) + 1;
        }
        
        public int Add(PositionClass position)
        {
            if(position is null)
                throw new ArgumentNullException(nameof(position));
            if (_Positions.Contains(position))
                return position.Id;
            
            position.Id = MaxFreeId++;
            _Positions.Add(position);
            return position.Id;
        }

        public bool Delete(int id)
        {
            var position = GetById(id);
            if(position is null)
            {
                _Logger.LogWarning("Попытка удаления отсутствующей должности сотрудника с Id: {0}", position.Id);
                return false;
            }
                
            _Positions.Remove(position);
            _Logger.LogInformation("Должность сотрудника Id: {0} была удалена", position.Id);
            return true;
        }

        public bool Edit(PositionClass position)
        {
            if (position is null)
                throw new ArgumentNullException(nameof(position));
            if (_Positions.Contains(position))
                return true;
            var db_position = GetById(position.Id);
            if(db_position is null)
            {
                _Logger.LogWarning("Попытка редактирования отсутствующей должности сотрудника с Id: {0}", position.Id);
                return false;
            }
                
            db_position.Name = position.Name;
            _Logger.LogInformation("Информация о должности сотрудника Id: {0} была изменена", position.Id);
            return true;
        }

        public IEnumerable<PositionClass> GetAll() => _Positions;
        

        public PositionClass? GetById(int id) => _Positions.FirstOrDefault(position => position.Id == id);

        //public PositionClass? GetByName(string name) => _Positions.FirstOrDefault(position => position.Name == name);
        //public string GetName(int id) => _Positions.FirstOrDefault(p => p.Id == id).Name;
    }
}
