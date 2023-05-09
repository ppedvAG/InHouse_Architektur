using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.CyanBayCars.Models;
using System.Reflection;

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

            result.Should().BeTrue();
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

            affRows.Should().Be(1);
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
                loadedCar.Should().NotBeNull();
                loadedCar.Model.Should().Be(car.Model);
                loadedCar.PS.Should().BeInRange(299, 700);
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

                loadedCar.Model.Should().Be(newModel);
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

                loadedCar.Should().BeNull();
            }
        }

        [Fact]
        public void Can_create_and_read_car_with_AutoFixture()
        {
            var fix = new Fixture();

            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var car = fix.Build<Car>().Without(x => x.Color).Create();

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Cars.Add(car);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loadedCar = con.Cars.Find(car.Id);
                loadedCar.Should().BeEquivalentTo(car, x => x.IgnoringCyclicReferences());
            }
        }
    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}
