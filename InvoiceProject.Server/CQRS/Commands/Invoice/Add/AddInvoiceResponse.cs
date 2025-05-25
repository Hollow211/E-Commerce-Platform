namespace Application.CQRS.Commands.Invoice.Add
{
    public class AddInvoiceResponse
    {
        public int id { get; set; }

        public bool isSuccess { get; set; }
    }
}
