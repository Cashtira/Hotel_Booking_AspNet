namespace _2._Domain.Interfaces.Repositories;

using _2._Domain.Entities;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllServicesAsync();

    Task<Service?> GetServiceByIdAsync(int serviceId);

    Task AddServiceAsync(Service service);

    Task UpdateServiceAsync(Service service);

    Task DeleteServiceByIdAsync(int serviceId);
}
