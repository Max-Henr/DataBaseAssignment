using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ContactFactory
{
    //Create
    public static ContactEntity Create(ContactRegistrationForm form)
    {
        return new ContactEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            PhoneNumber = form.PhoneNumber
        };
    }
    //Read
    public static IEnumerable<Contact> Create(IEnumerable<ContactEntity> entities)
    {
        return entities.Select(Create);
    }

    //Update
    public static ContactEntity UpdateEntity(ContactEntity entity , ContactUpdateForm form)
    {
        entity.Id = form.Id;
        entity.FirstName = form.FirstName;
        entity.LastName = form.LastName;
        entity.Email = form.Email;
        entity.PhoneNumber = form.PhoneNumber;
        return entity;
    }
    public static Contact Create(ContactEntity entity) 
    {
        return new Contact
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber
        };
    }
}
