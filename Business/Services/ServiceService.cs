using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    //Create
    public async Task<bool> CreateService(ServiceRegistrationForm form)
    {
        await _serviceRepository.BeginTransactionAsync();
        try
        {
            var service = ServiceFactory.Create(form);
            var result = await _serviceRepository.CreateAsync(service);
            await _serviceRepository.CommitTransactionAsync();
            await _serviceRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating Service :: {ex.Message}");
            await _serviceRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Service>> GetAllServices()
    {
        var services = await _serviceRepository.GetAllAsync();
        return ServiceFactory.Create(services);
    }

    //Update
    public async Task<Service> UpdateService(ServiceUpdateForm form)
    {
        await _serviceRepository.BeginTransactionAsync();
        try
        {
            var service = await _serviceRepository.GetByIdAsync(x => x.Id == form.Id);
            if (service == null)
                return null!;
            service = ServiceFactory.UpdateEntity(service, form);
            await _serviceRepository.UpdateAsync(x => x.Id == form.Id, service);
            if (service != null)
            {
                await _serviceRepository.CommitTransactionAsync();
                await _serviceRepository.SaveChangesAsync();
                return ServiceFactory.Create(service);
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating Service :: {ex.Message}");
            await _serviceRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    //Delete
    public async Task<bool> DeleteService(int id)
    {
        await _serviceRepository.BeginTransactionAsync();
        try
        {
            var service = await _serviceRepository.GetByIdAsync(x => x.Id == id);
            if (service == null)
                return false;
            var result = await _serviceRepository.DeleteAsync(x => x.Id == id);
            await _serviceRepository.CommitTransactionAsync();
            await _serviceRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting Service :: {ex.Message}");
            await _serviceRepository.RollbackTransactionAsync();
            return false;
        }
    }
}