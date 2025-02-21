using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<bool> CreateService(ServiceRegistrationForm form);
        Task<IEnumerable<Service>> GetAllServices();
        Task<Service?> UpdateService(ServiceUpdateForm form);
        Task<bool> DeleteService(int id);
    }
}