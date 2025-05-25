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
        public async Task<Product?> GetById(int id) => await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<ICollection<Product>> GetAllProducts() => await _context.Products.ToListAsync();

        public async Task CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            //await _context.ProductUnits.AddRangeAsync(product.Units);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
