namespace ppedv.CyanBayCars.Models
{
    public class Car : Entity
    {
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public CarType CarType { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Seats { get; set; }
        public int PS { get; set; }

        public virtual ICollection<Rent> Rents { get; set; } = new HashSet<Rent>();
    }
}