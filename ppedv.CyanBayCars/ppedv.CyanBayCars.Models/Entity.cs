namespace ppedv.CyanBayCars.Models;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}