using System.Diagnostics;
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

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    //Create
    public async Task<bool> CreateCustomer(CustomerRegistrationForm form)
    {
        await _customerRepository.BeginTransactionAsync();
        try
        {
            var customer = CustomerFactory.Create(form);
            var result = await _customerRepository.CreateAsync(customer);
            await _customerRepository.CommitTransactionAsync();
            await _customerRepository.SaveChangesAsync();
            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating Customer :: {ex.Message}");
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        var customers = await _customerRepository.GetAllAsync();
        return CustomerFactory.Create(customers);
    }

    //Update
    public async Task<Customer?> UpdateCustomer(CustomerUpdateForm form)
    {
        await _customerRepository.BeginTransactionAsync();
        try
        {
            var customer = await _customerRepository.GetByIdAsync(x => x.Id == form.Id);
            if (customer == null)
                return null!;

            customer = CustomerFactory.UpdateEntity(customer, form);
            await _customerRepository.UpdateAsync(x => x.Id == form.Id, customer);

            if (customer != null)
            {
                await _customerRepository.CommitTransactionAsync();
                await _customerRepository.SaveChangesAsync();
                return CustomerFactory.Create(customer);
            }
            return null;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating Customer :: {ex.Message}");
            await _customerRepository.RollbackTransactionAsync();
            return null;
        }
    }

    //Delete
    public async Task<bool> DeleteCustomer(int id)
    {
        await _customerRepository.BeginTransactionAsync();
        try
        {
            var contact = await _customerRepository.GetByIdAsync(x => x.Id == id);
            if (contact == null)
                return false;
            var result = await _customerRepository.DeleteAsync(x => x.Id == id);
            await _customerRepository.CommitTransactionAsync();
            await _customerRepository.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting Customer :: {ex.Message}");
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }
}
