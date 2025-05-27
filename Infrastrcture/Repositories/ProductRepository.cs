using Domain.Aggregates;
using Domain.Aggregates.ProductAggregate;
using Domain.Shared.Interfaces;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrcture.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) => _context = context;
        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.Include(x => x.Units).ThenInclude(x => x.Unit).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Include(x => x.Units).ThenInclude(x => x.Unit).ToListAsync();
        }

        public async Task CreateProduct(Product product) => await _context.Products.AddAsync(product);

        public async Task SaveChanges() => await _context.SaveChangesAsync();

        public async Task<List<Product>> GetProductsFromIds(List<int> ids)
        {
            return await _context.Products.Include(x => x.Units).
                ThenInclude(x => x.Unit).
                Where(p => ids.Contains(p.Id)).
                Distinct().
                ToListAsync();
        }
    }
}
