namespace Business.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ContactId { get; set; }
    public Contact Contact { get; set; } = null!;

}
