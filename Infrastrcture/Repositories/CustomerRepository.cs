using Domain.AggregateNodes;
using Infrastrcture.DatabaseContexts;
using Domain.Shared.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Shared;

namespace Infrastrcture.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public Task DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            return Task.CompletedTask;
        }

        public async Task<Customer?> GetByEmail(string email)
        {
            return await _context.Customers
                        .Include(c => c.Invoices)
                        .FirstOrDefaultAsync(c => c.CustomerEmail == email);
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _context.Customers
                        .Include(c => c.Invoices)
                        .FirstOrDefaultAsync(c => c.id == id);
        }
    }
}
