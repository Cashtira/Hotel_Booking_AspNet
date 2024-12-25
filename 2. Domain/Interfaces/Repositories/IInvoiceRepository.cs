namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllInvoicesAsync();

    Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);

    Task AddInvoiceAsync(Invoice invoice);

    Task UpdateInvoiceAsync(Invoice invoice);

    Task DeleteInvoiceByIdAsync(int invoiceId);
}
