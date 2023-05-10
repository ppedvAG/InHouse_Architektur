using ppedv.CyanBayCars.Models;

namespace ppedv.CyanBayCars.Models.Contracts
{
    public interface ICarService
    {
        IReadOnlyList<Car> GetAllAvailableCars();
    }
}