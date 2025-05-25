using Domain.AggregateNodes;
using Domain.Aggregates.InvoiceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllCustomerInvoices(Customer customer);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(Invoice invoice);
        Task SaveChanges();
    }
}
