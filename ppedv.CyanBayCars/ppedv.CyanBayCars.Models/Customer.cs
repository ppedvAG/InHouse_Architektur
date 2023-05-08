namespace ppedv.CyanBayCars.Models;

public class Customer : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime BirthDate { get; set; }
}