using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Data.EfCore
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public ICarRepository CarRepo => new EfCarRepository(context);

        public IRepository<Rent> RentRepo => new EfRepository<Rent>(context);

        public IRepository<Customer> CustomerRepo => new EfRepository<Customer>(context);

        EfContext context;

        public EfUnitOfWork(string conString)
        {
            context = new EfContext(conString);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
