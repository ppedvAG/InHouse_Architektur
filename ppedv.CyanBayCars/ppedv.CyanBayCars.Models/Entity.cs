namespace ppedv.CyanBayCars.Models;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}