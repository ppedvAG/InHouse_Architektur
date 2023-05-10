namespace ppedv.CyanBayCars.Models.Contracts
{
    public interface ICarRepository : IRepository<Car>
    {
        IReadOnlyList<Car> GetAllCarsThatHaveSpecialNeeds();
    }
}
