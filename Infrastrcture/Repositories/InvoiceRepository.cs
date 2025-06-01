using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.AggregateNodes;
using Domain.Aggregates.InvoiceAggregate;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastrcture.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context) => _context = context;

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.InvoiceItem.AddRangeAsync(invoice.Items);
        }

        public Task DeleteInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            return Task.CompletedTask;
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .ThenInclude(ii => ii.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            return Task.CompletedTask;
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();

        public async Task<List<Invoice>> GetInvoicesByCustomerId(int customerId)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .ThenInclude(ii => ii.Product)
                .Where(i => i.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
