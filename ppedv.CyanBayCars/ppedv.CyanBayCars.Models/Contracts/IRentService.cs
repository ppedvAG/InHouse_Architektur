using ppedv.CyanBayCars.Models;

namespace ppedv.CyanBayCars.Models.Contracts
{
    public interface IRentService
    {
        void StartRent(Rent rent);
        void EndRent(Rent rent);

        
    }
}