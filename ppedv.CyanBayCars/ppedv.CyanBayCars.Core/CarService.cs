using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Core
{
    public class CarService
    {
        private readonly IRepository repository;

        public CarService(IRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));

            this.repository = repository;
        }

        public IReadOnlyList<Car> GetAllAvailableCars()
        {
            return repository.Query<Car>().Where(car => car.Rents.All(x => x.EndDate != null)).ToList();
        }
    }
}