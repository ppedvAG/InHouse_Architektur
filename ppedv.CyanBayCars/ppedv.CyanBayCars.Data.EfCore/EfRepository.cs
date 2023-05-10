using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Data.EfCore
{

    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected EfContext context;

        public EfRepository(EfContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return context.Set<T>();
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
