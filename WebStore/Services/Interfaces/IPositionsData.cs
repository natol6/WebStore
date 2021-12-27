using WebStore.Domain.References;

namespace WebStore.Services.Interfaces
{
    public interface IPositionsData
    {
        IEnumerable<PositionClass> GetAll();
        PositionClass? GetById(int id);
        PositionClass? GetByName(string name);
        int Add(PositionClass position);
        bool Edit(PositionClass position);
        bool Delete(int id);
    }
}
