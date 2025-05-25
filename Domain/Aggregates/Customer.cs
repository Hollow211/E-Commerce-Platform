using Domain.Aggregates.InvoiceAggregate;
using Domain.Shared;
using FluentResults;

namespace Domain.AggregateNodes;

public class Customer : Entity<int>
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public static Result<Customer> CreateCustomer(string name, string email, string address, string phoneNumber)
    {
        if (CheckValidation(name, email, address, phoneNumber) == false)
        {
            return Result.Fail("Invalid or missing inputs");
        }

        Customer newCustomer =  new Customer
        {
            Name = name,
            Email = email,
            Address = address,
            PhoneNumber = phoneNumber
        };

        return Result.Ok(newCustomer);
    }

    public bool CanDeleteCustomer(int customerId)
    {
        bool isThereUnPaid = Invoices.Any(inv => !inv.isPaid);
        return !isThereUnPaid;
    }

    private static bool CheckValidation(string name, string email, string address, string phoneNumber)
    {
       if (string.IsNullOrWhiteSpace(name))
            return false;
        if (string.IsNullOrWhiteSpace(email))
            return false;
        if (string.IsNullOrWhiteSpace(address))
            return false;
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;
        return true;
    }
}
