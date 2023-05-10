using Microsoft.EntityFrameworkCore;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Data.EfCore
{
    public class EfCarRepository : EfRepository<Car>, ICarRepository
    {
        public EfCarRepository(EfContext context) : base(context)
        {
        }

        public IReadOnlyList<Car> GetAllCarsThatHaveSpecialNeeds()
        {
            return context.Cars.FromSql($"SELECT * FROM Cars").ToList();
        }
    }
}
