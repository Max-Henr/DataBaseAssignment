using System.Diagnostics;
using System.Runtime.InteropServices;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    //Create
    public async Task<bool> CreateEmployee(EmployeeRegistrationForm form)
    {
        await _employeeRepository.BeginTransactionAsync();
        try
        {
            var employee = EmployeeFactory.Create(form);
            var result = await _employeeRepository.CreateAsync(employee);
            await _employeeRepository.CommitTransactionAsync();
            await _employeeRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating Employee :: {ex.Message}");
            await _employeeRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return EmployeeFactory.Create(employees);
    }

    //Update
    public async Task<Employee> UpdateEmployee(EmployeeUpdateForm form)
    {
        await _employeeRepository.BeginTransactionAsync();
        try
        {
            var employee = await _employeeRepository.GetByIdAsync(x => x.Id == form.Id);
            if (employee == null)
                return null!;
            employee = EmployeeFactory.UpdateEntity(employee, form);
            await _employeeRepository.UpdateAsync(x => x.Id == form.Id, employee);
            if (employee != null)
            {
                await _employeeRepository.CommitTransactionAsync();
                await _employeeRepository.SaveChangesAsync();
                return EmployeeFactory.Create(employee);
            }
            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating Employee :: {ex.Message}");
            await _employeeRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    //Delete
    public async Task<bool> DeleteEmployee(int id)
    {
        await _employeeRepository.BeginTransactionAsync();
        try
        {
            var contact = await _employeeRepository.GetByIdAsync(x => x.Id == id);
            if (contact == null)
                return false;
            var result = await _employeeRepository.DeleteAsync(x => x.Id == id);
            await _employeeRepository.CommitTransactionAsync();
            await _employeeRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting Employee :: {ex.Message}");
            await _employeeRepository.RollbackTransactionAsync();
            return false;
        }
    }
}

