using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Core
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
        }


        public IReadOnlyList<Car> GetAllAvailableCars()
        {
            return unitOfWork.CarRepo.Query().Where(car => car.Rents.All(x => x.EndDate != null)).ToList();
        }
    }
}