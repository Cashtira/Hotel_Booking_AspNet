namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper) : IInvoiceService
{
    private readonly IInvoiceRepository invoiceRepository = invoiceRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<InvoiceDTO>> GetAllInvoicesAsync()
    {
        var invoices = await this.invoiceRepository.GetAllInvoicesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<List<InvoiceDTO>>(invoices);
    }

    public async Task<InvoiceDTO?> GetInvoiceByIdAsync(int invoiceId)
    {
        var invoice = await this.invoiceRepository.GetInvoiceByIdAsync(invoiceId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        return this.mapper.Map<InvoiceDTO>(invoice);
    }

    public async Task AddInvoiceAsync(InvoiceDTO invoiceDto)
    {
        var invoice = this.mapper.Map<Invoice>(invoiceDto);
        await this.invoiceRepository.AddInvoiceAsync(invoice).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task UpdateInvoiceAsync(InvoiceDTO invoiceDto)
    {
        var invoice = this.mapper.Map<Invoice>(invoiceDto);
        await this.invoiceRepository.UpdateInvoiceAsync(invoice).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteInvoiceByIdAsync(int invoiceId)
    {
        await this.invoiceRepository.DeleteInvoiceByIdAsync(invoiceId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
}
