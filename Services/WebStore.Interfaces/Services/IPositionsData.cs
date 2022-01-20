using WebStore.Domain.References;

namespace WebStore.Interfaces.Services
{
    public interface IPositionsData
    {
        IEnumerable<PositionClass> GetAll();
        PositionClass? GetById(int id);
        int Add(PositionClass position);
        bool Edit(PositionClass position);
        bool Delete(int id);
    }
}
