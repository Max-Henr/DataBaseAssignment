using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ContactRepository(DataContext context) : BaseRepository<ContactEntity>(context), IContactRepository
{
    private readonly DataContext _context = context;
}
