using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Data.EfCore
{
    public class EfRepository : IRepository
    {
        EfContext context;

        public EfRepository(string conString)
        {
            context = new EfContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            context.Update(entity);
        }
    }
}
