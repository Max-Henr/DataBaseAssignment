using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    //Create
    public static CustomerEntity Create(CustomerRegistrationForm form)
    {
        return new CustomerEntity
        {
            Name = form.Name,
            ContactId = form.ContactId
        };
    }
        //Read
    public static IEnumerable<Customer> Create(IEnumerable<CustomerEntity> entities)
    {
        return entities.Select(x => new Customer
        {
            Id = x.Id,
            Name = x.Name,
            ContactId = x.ContactId,
            Contact = new Contact
            {
                Id = x.Contact.Id,
                FirstName = x.Contact.FirstName,
                LastName = x.Contact.LastName,
                Email = x.Contact.Email,
                PhoneNumber = x.Contact.PhoneNumber
            }
        });
    }
    //Update
    public static CustomerEntity UpdateEntity(CustomerEntity entity, CustomerUpdateForm form)
    {
        entity.Id = form.Id;
        entity.Name = form.Name;
        entity.ContactId = form.ContactId;
        return entity;
    }
    public static Customer Create(CustomerEntity entity)
    {
        return new Customer
        {
            Name = entity.Name,
            ContactId = entity.ContactId
        };

    }
}