using System.Diagnostics;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeEntity>(context), IEmployeeRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Employees
                .Include(e => e.Role)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Getting All {nameof(EmployeeEntity)} entities :: {ex.Message}");
            return null!;
        }
    }
}
