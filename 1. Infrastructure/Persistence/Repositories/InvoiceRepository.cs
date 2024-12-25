namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class InvoiceRepository(ApplicationDbContext dbContext) : IInvoiceRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
    {
        return await this.dbContext.Invoices.ToListAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
    {
        return await this.dbContext.Invoices.FirstOrDefaultAsync(e => e.InvoiceId == invoiceId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task AddInvoiceAsync(Invoice invoice)
    {
        this.dbContext.Add(invoice);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }
    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        this.dbContext.Update(invoice);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
    }

    public async Task DeleteInvoiceByIdAsync(int invoiceId)
    {
        var invoice = await this.GetInvoiceByIdAsync(invoiceId).ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        if (invoice != null)
        {
            this.dbContext.Remove(invoice);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
        }
    }
}
