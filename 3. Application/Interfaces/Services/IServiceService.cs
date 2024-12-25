namespace _3._Application.Interfaces.Services;

using _3._Application.DTOs;

public interface IServiceService
{
    Task<List<ServiceDTO>> GetAllServicesAsync();

    Task<ServiceDTO?> GetServiceByIdAsync(int serviceId);

    Task AddServiceAsync(ServiceDTO serviceDto);

    Task UpdateServiceAsync(ServiceDTO serviceDto);

    Task DeleteServiceByIdAsync(int serviceId);
}
