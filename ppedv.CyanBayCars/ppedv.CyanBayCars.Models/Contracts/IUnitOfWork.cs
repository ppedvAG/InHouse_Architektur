namespace ppedv.CyanBayCars.Models.Contracts
{
    public interface IUnitOfWork
    {
        ICarRepository CarRepo { get; }
        IRepository<Rent> RentRepo { get; }
        IRepository<Customer> CustomerRepo { get; }

        int SaveChanges();
    }
}
