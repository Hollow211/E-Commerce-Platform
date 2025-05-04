using Domain.AggregateNodes;
using Domain.Shared.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Shared;
using Infrastructure.DatabaseContexts;
using Domain.Aggregates.InvoiceAggregate;

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
                        .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _context.Customers
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            return Task.CompletedTask;
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();

        public async Task<List<Invoice>> GetCustomerInvoices(int id)
        {
            return await _context.Invoices.Where(x => x.CustomerId == id).ToListAsync();
        }
    }
}
