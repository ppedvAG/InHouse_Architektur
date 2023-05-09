using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Core
{
    public class CarService
    {
        private readonly IRepository repository;

        public CarService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Car> GetAllAvailableCars()
        {
            return repository.GetAll<Car>().Where(IsCarAvailable);
        }

        public static bool IsCarAvailable(Car car)
        {
            return car.Rents.All(x => x.EndDate != null);
        }
    }
}