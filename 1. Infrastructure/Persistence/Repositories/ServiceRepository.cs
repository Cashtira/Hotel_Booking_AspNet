namespace _1._Infrastructure.Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

public class ServiceRepository(ApplicationDbContext dbContext) : IServiceRepository
{
    private readonly ApplicationDbContext dbContext = dbContext ?? new ApplicationDbContext();

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await this.dbContext.Services.ToListAsync().ConfigureAwait(true);
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await this.dbContext.Services.FirstOrDefaultAsync(e => e.ServiceId == serviceId).ConfigureAwait(true);
    }

    public async Task AddServiceAsync(Service service)
    {
        this.dbContext.Add(service);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(true);
    }
    public async Task UpdateServiceAsync(Service service)
    {
        this.dbContext.Update(service);
        await this.dbContext.SaveChangesAsync().ConfigureAwait(true);
    }

    public async Task DeleteServiceByIdAsync(int serviceId)
    {
        var service = await this.GetServiceByIdAsync(serviceId).ConfigureAwait(true);
        if (service != null)
        {
            this.dbContext.Remove(service);
            await this.dbContext.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
