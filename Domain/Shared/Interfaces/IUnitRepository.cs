using Domain.Aggregates;
using Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces
{
    public interface IUnitRepository
    {
        Task<Unit?> GetById(int id);

        Task<ICollection<Unit>> GetAllUnits();

        Task<ICollection<Unit>> GetByIds(IEnumerable<int> ids);

        Task<List<Unit>> GetUnitsFromIds(List<int> unitsIds);
    }
}
