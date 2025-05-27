
using Domain.Aggregates;
using Domain.Aggregates.ProductAggregate;
using Domain.Shared.Interfaces;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastrcture.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;
        public UnitRepository(ApplicationDbContext context) => _context = context;

        public async Task<ICollection<Unit>> GetAllUnits()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit?> GetById(int id)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Unit>> GetByIds(IEnumerable<int> ids)
        {
            return await _context.Units
            .Where(u => ids.Contains(u.Id))
            .ToListAsync();
        }

        public async Task<List<Unit>> GetUnitsFromIds(List<int> unitsIds)
        {
            return await _context.Units
                .Where(x => unitsIds.Contains(x.Id))
                .Distinct()
                .ToListAsync();
        }
    }
}
