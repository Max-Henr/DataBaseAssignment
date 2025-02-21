using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces
{
    public interface IContactService
    {
        Task<bool> CreateContact(ContactRegistrationForm form);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<ContactEntity> GetContactById(int id);
        Task<Contact?> UpdateContact(ContactUpdateForm form);
        Task<bool> DeleteContact(int id);
    }
}