namespace _3._Application.Services;

using _2._Domain.Entities;
using _2._Domain.Interfaces.Repositories;
using _3._Application.DTOs;
using _3._Application.Interfaces.Services;
using AutoMapper;

public class ServiceService(IServiceRepository serviceRepository, IMapper mapper) : IServiceService
{
    private readonly IServiceRepository serviceRepository = serviceRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<ServiceDTO>> GetAllServicesAsync()
    {
        var services = await this.serviceRepository.GetAllServicesAsync().ConfigureAwait(true);
        return this.mapper.Map<List<ServiceDTO>>(services);
    }

    public async Task<ServiceDTO?> GetServiceByIdAsync(int serviceId)
    {
        var service = await this.serviceRepository.GetServiceByIdAsync(serviceId).ConfigureAwait(true);
        return this.mapper.Map<ServiceDTO>(service);
    }

    public async Task AddServiceAsync(ServiceDTO serviceDto)
    {
        var service = this.mapper.Map<Service>(serviceDto);
        await this.serviceRepository.AddServiceAsync(service).ConfigureAwait(true);
    }

    public async Task UpdateServiceAsync(ServiceDTO serviceDto)
    {
        var service = this.mapper.Map<Service>(serviceDto);
        await this.serviceRepository.UpdateServiceAsync(service).ConfigureAwait(true);
    }

    public async Task DeleteServiceByIdAsync(int serviceId)
    {
        await this.serviceRepository.DeleteServiceByIdAsync(serviceId).ConfigureAwait(true);
    }
}
