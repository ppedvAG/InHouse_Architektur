namespace ppedv.CyanBayCars.Models.Contracts
{
    public interface IRepository<T> where T : Entity
    {
        T? GetById(int id);
        IQueryable<T> Query();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }


    public interface ICarRepository : IRepository<Car>
    {
        IReadOnlyList<Car> GetAllCarsThatHaveSpecialNeeds();
    }

    public interface IUnitOfWork
    {
        ICarRepository CarRepo { get; }
        IRepository<Rent> RentRepo { get; }
        IRepository<Customer> CustomerRepo { get; }

        int SaveChanges();
    }
}
