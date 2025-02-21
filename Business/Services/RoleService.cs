using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    //Create
    public async Task<bool> CreateRole(RoleRegistrationForm form)
    {
        await _roleRepository.BeginTransactionAsync();
        try
        {
            var role = RoleFactory.Create(form);
            var result = await _roleRepository.CreateAsync(role);
            await _roleRepository.CommitTransactionAsync();
            await _roleRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating Role :: {ex.Message}");
            await _roleRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        var roles = await _roleRepository.GetAllAsync();

        return RoleFactory.Create(roles);

    }

    //Update
    public async Task<Role> UpdateRole(RoleUpdateForm form)
    {
        await _roleRepository.BeginTransactionAsync();
        try
        {
            var role = await _roleRepository.GetByIdAsync(x => x.Id == form.Id);
            if (role == null)
                return null!;
            role = RoleFactory.UpdateEntity(role, form);
            await _roleRepository.UpdateAsync(x => x.Id == form.Id, role);
            if (role != null)
            {
                await _roleRepository.CommitTransactionAsync();
                await _roleRepository.SaveChangesAsync();
                return RoleFactory.Create(role);
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating Role :: {ex.Message}");
            await _roleRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    //Delete
    public async Task<bool> DeleteRole(int id)
    {
        await _roleRepository.BeginTransactionAsync();
        try
        {
            var role = await _roleRepository.GetByIdAsync(x => x.Id == id);
            if (role == null)
                return false;
            var result = await _roleRepository.DeleteAsync(x => x.Id == id);
            await _roleRepository.CommitTransactionAsync();
            await _roleRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting Role :: {ex.Message}");
            await _roleRepository.RollbackTransactionAsync();
            return false;
        }
    }
}