using Data.Entities;

namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
    public Service Service { get; set; } = null!;   
}
