using Domain.AggregateNodes;
using Domain.Aggregates.InvoiceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetById(int id);

        Task<Customer?> GetByEmail(string email);

        Task AddCustomer(Customer customer);

        Task DeleteCustomer(Customer customer);

        Task UpdateCustomer(Customer customer);

        Task<List<Invoice>> GetCustomerInvoices(int id);

        Task SaveChanges();
    }
}
