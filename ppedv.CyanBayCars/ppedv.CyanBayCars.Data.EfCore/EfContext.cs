using Microsoft.EntityFrameworkCore;
using ppedv.CyanBayCars.Models;

namespace ppedv.CyanBayCars.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rent> Rents { get; set; }

        readonly string conString;

        public EfContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }
    }
}