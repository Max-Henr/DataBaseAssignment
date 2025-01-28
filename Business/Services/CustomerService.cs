
using Business.Dtos;
using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class CustomerService(DataContext context, IContactService contactService) : ICustomerService
{
    private readonly DataContext _context = context;
    private readonly IContactService _contactService = contactService;

    public CustomerEntity CreateCustomer(CustomerEntity customerEntity)
    {

        _context.Customers.Add(customerEntity);
        _context.SaveChanges();

        return customerEntity;
    }

    public IEnumerable<CustomerEntity> GetCustomers()
    {
        var customers = _context.Customers.Include(x => x.Contact).ToList();
        return customers;
    }

    public CustomerEntity GetCustomerById(int id)
    {
        var customerEntity = _context.Customers.FirstOrDefault(x => x.Id == id);
        if (customerEntity != null)
            return customerEntity;
        else
            return null!;
    }

    public CustomerEntity UpdateCustomer(CustomerEntity CustomerEntity)
    {
        _context.Customers.Update(CustomerEntity);
        _context.SaveChanges();

        return CustomerEntity;
    }

    public bool DeleteCustomerById(int id)
    {
        var customerEntity = _context.Customers.FirstOrDefault(x => x.Id == id);
        if (customerEntity != null)
        {
            _context.Remove(customerEntity);
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }
}
