using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRole(RoleRegistrationForm form);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> UpdateRole(RoleUpdateForm form);
        Task<bool> DeleteRole(int id);
    }
}