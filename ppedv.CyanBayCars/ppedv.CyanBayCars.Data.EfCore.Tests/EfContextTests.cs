using ppedv.CyanBayCars.Models;

namespace ppedv.CyanBayCars.Data.EfCore.Tests
{
    public class EfContextTests
    {
        //https://www.connectionstrings.com/sql-server/
        string conString = "Server=(localdb)\\mssqllocaldb;Database=CyanBayCars_Dev;Trusted_Connection=true;";

        [Fact]
        public void Can_create_new_DB()
        {
            var con = new EfContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
        }

        [Fact]
        public void Can_add_Car()
        {
            var car = new Car
            {
                Model = "Model X",
                Manufacturer = "Tesla",
                CarType = CarType.SUV,
                Color = "Red",
                Seats = 5,
                PS = 400,
            };
            var con = new EfContext(conString);
            con.Database.EnsureCreated();

            con.Cars.Add(car);
            int affRows = con.SaveChanges();

            Assert.Equal(1, affRows);
        }

        [Fact]
        public void Can_Read_Car()
        {
            var car = new Car
            {
                Model = $"Model_{Guid.NewGuid()}",
                Manufacturer = "Tesla",
                CarType = CarType.SUV,
                Color = "Red",
                Seats = 5,
                PS = 400,
            };

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loadedCar = con.Cars.Find(car.Id);
                Assert.NotNull(loadedCar);
                Assert.Equal(car.Model, loadedCar.Model);
            }
        }

        [Fact]
        public void Can_Update_Car()
        {
            var car = new Car
            {
                Model = $"Model_{Guid.NewGuid()}",
                Manufacturer = "Tesla",
                CarType = CarType.SUV,
                Color = "Red",
                Seats = 5,
                PS = 400,
            };

            var newModel = $"NewModel_{Guid.NewGuid()}";

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loadedCar = con.Cars.Find(car.Id);
                loadedCar.Model = newModel;
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loadedCar = con.Cars.Find(car.Id);
                Assert.Equal(newModel, loadedCar.Model);
            }
        }

        [Fact]
        public void Can_Delete_Car()
        {
            var car = new Car
            {
                Model = $"Model_{Guid.NewGuid()}",
                Manufacturer = "Tesla",
                CarType = CarType.SUV,
                Color = "Red",
                Seats = 5,
                PS = 400,
            };

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loadedCar = con.Cars.Find(car.Id);
                con.Cars.Remove(loadedCar);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loadedCar = con.Cars.Find(car.Id);
                Assert.Null(loadedCar);
            }
        }
    }
}