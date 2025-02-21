using System.Diagnostics;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    private readonly DataContext _context = context;

    //Read
    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            
            return await _context.Customers
                .Include(c => c.Contact)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Getting All {nameof(CustomerEntity)} entities :: {ex.Message}");
            return null!;
        }
    }
}
