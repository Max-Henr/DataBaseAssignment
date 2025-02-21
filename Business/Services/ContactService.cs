
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;

    //Create
    public async Task<bool> CreateContact(ContactRegistrationForm form)
    {
        await _contactRepository.BeginTransactionAsync();
        try
        {
            var contact = ContactFactory.Create(form);

            var result = await _contactRepository.CreateAsync(contact);
            await _contactRepository.CommitTransactionAsync();
            await _contactRepository.SaveChangesAsync();
            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Creating Contact :: {ex.Message}");
            await _contactRepository.RollbackTransactionAsync();
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<Contact>> GetAllContacts()
    {
        var contacts = await _contactRepository.GetAllAsync();
        return ContactFactory.Create(contacts);
    }

    public async Task<ContactEntity> GetContactById(int id)
    {
        return await _contactRepository.GetByIdAsync(x => x.Id == id);
    }

    //Update
    public async Task<Contact?> UpdateContact(ContactUpdateForm form)
    {
        await _contactRepository.BeginTransactionAsync();
        try
        {
            var contact = await _contactRepository.GetByIdAsync(x => x.Id == form.Id);
            if (contact == null)
                return null!;

            contact = ContactFactory.UpdateEntity(contact, form);
            await _contactRepository.UpdateAsync(x => x.Id == form.Id, contact);

            if (contact != null)
            {
                await _contactRepository.CommitTransactionAsync();
                await _contactRepository.SaveChangesAsync();
                return ContactFactory.Create(contact);
            }
                return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Updating Contact :: {ex.Message}");
            await _contactRepository.RollbackTransactionAsync();
            return null;
        }
    }

    //Delete
    public async Task<bool> DeleteContact(int id)
    {
        await _contactRepository.BeginTransactionAsync();
        try
        {
        var contact = await _contactRepository.GetByIdAsync(x => x.Id == id);
        if (contact == null)
            return false;
        var result = await _contactRepository.DeleteAsync(x => x.Id == id);
            await _contactRepository.CommitTransactionAsync();
            await _contactRepository.SaveChangesAsync();
        return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Deleting Project :: {ex.Message}");
            await _contactRepository.RollbackTransactionAsync();
            return false;
        }
    }
}
