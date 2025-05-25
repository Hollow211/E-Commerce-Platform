
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

        public Task<ICollection<Unit>> GetAllUnits()
        {
            throw new NotImplementedException();
        }

        public async Task<Unit?> GetById(int id)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
