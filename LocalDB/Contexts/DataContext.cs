using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ContactEntity> Contacts { get; set; }

    public DbSet<CustomerEntity> Customers { get; set; }

    public DbSet<EmployeeEntity> Employees { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    public DbSet<ProjectEntity> Projects { get; set; }

    public DbSet<ServiceEntity> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasOne(c => c.Contact)
            .WithMany(c => c.Customers)
            .HasForeignKey(c => c.ContactId);
    }
}
