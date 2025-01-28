

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string ProjectName { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Status { get; set; } = null!;
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    [Required]
    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
    public int EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;

}
