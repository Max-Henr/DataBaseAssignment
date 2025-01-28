using Business.Dtos;
using Data.Entities;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        CustomerEntity CreateCustomer(CustomerEntity customerEntity);
        bool DeleteCustomerById(int id);
        CustomerEntity GetCustomerById(int id);
        IEnumerable<CustomerEntity> GetCustomers();
        CustomerEntity UpdateCustomer(CustomerEntity CustomerEntity);
    }
}