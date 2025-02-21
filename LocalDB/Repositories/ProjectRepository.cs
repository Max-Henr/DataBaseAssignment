using System.Diagnostics;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Projects
                .Include(e => e.Customer)
                .Include(e => e.Service)
                .Include(e => e.Employee)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error Getting All {nameof(EmployeeEntity)} entities :: {ex.Message}");
            return null!;
        }
    }
}

