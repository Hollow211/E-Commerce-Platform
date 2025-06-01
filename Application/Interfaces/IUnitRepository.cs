using Domain.Aggregates;


namespace Application.Interfaces
{
    public interface IUnitRepository
    {
        Task<Unit?> GetById(int id);

        Task<ICollection<Unit>> GetAllUnits();

        Task<ICollection<Unit>> GetByIds(IEnumerable<int> ids);

        Task<List<Unit>> GetUnitsFromIds(List<int> unitsIds);
    }
}
