namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IInvoiceService
{
    Task<List<InvoiceDTO>> GetAllInvoicesAsync();

    Task<InvoiceDTO?> GetInvoiceByIdAsync(int invoiceId);

    Task AddInvoiceAsync(InvoiceDTO invoiceDto);

    Task UpdateInvoiceAsync(InvoiceDTO invoiceDto);

    Task DeleteInvoiceAsync(int invoiceId);
}
