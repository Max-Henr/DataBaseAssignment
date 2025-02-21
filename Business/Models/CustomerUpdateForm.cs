namespace Business.Models;

public class CustomerUpdateForm
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ContactId { get; set; }
}
