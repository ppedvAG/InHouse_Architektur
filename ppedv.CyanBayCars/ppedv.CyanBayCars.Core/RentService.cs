using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Core
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork uow;
        private readonly ICarService carService;

        public RentService(IUnitOfWork unitOfWork, ICarService carService)
        {
            this.uow = unitOfWork;
            this.carService = carService;
        }

        public void StartRent(Rent rent)
        {
            ArgumentNullException.ThrowIfNull(rent);
            ArgumentNullException.ThrowIfNull(rent.Car);
            ArgumentNullException.ThrowIfNull(rent.Customer);

            if (!carService.GetAllAvailableCars().Contains(rent.Car))
                throw new InvalidOperationException("Das Car ist nicht available");

            rent.StartDate = DateTime.Now;
            //todo insert new Rent logic here

            uow.RentRepo.Add(rent);
            uow.SaveChanges();
        }

        public void EndRent(Rent rent)
        {
            throw new NotImplementedException();
        }


    }
}
