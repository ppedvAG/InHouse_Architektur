namespace ppedv.CyanBayCars.Models;

public class Rent : Entity
{
    public virtual required Car Car { get; set; }
    public virtual required Customer Customer { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int StartKm { get; set; }
    public int? EndKm { get; set; }
    public string StartLocation { get; set; } = string.Empty;
    public string? EndLocation { get; set; }
    public decimal TotalPrice { get; set; }
    public string PriceUnit { get; set; } = "USD";
}