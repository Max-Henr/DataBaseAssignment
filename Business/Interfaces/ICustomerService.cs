using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(CustomerRegistrationForm form);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> UpdateCustomer(CustomerUpdateForm form);
        Task<bool> DeleteCustomer(int id);
    }
}