using Data.Entities;

namespace Business.Dtos;

public class ProjectRegistrationForm
{
    public string ProjectName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }

}
