using FluentAssertions;
using Moq;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.Core.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public void CarService_with_null_as_unitOfWork_should_throw()
        {
            var action = () => new CarService(null!);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetAllAvailableCars_no_cars_should_return_empty_list()
        {
            var repoMock = new Mock<IRepository<Car>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.CarRepo).Returns(repoMock.Object);
            var carService = new CarService(uowMock.Object);

            var result = carService.GetAllAvailableCars();

            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAllAvailableCars_3_cars_3_open_rents_should_return_empty_list()
        {
            var repoMock = new Mock<IRepository<Car>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.CarRepo).Returns(repoMock.Object);
            repoMock.Setup(x => x.Query()).Returns(() =>
            {
                var c1 = new Car() { Id = 1, Model = "C1" };
                c1.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-5), Car = c1, Customer = new Customer() });

                var c2 = new Car() { Id = 2, Model = "C2" };
                c2.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-5), Car = c2, Customer = new Customer() });

                var c3 = new Car() { Id = 3, Model = "C3" };
                c3.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-2), Car = c3, Customer = new Customer() });

                return new[] { c1, c2, c3 }.AsQueryable();
            });

            var carService = new CarService(uowMock.Object);

            var result = carService.GetAllAvailableCars();

            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAllAvailableCars_3_cars_2_open_rent_should_return_correct_car()
        {
            var carService = new CarService(new TestUnitOfWork());

            var result = carService.GetAllAvailableCars();

            result.Should().HaveCount(1).And.AllSatisfy(x => x.Id.Should().Be(2));
        }

        [Fact]
        public void GetAllAvailableCars_3_cars_2_open_rent_should_return_correct_car_moq()
        {
            var repoMock = new Mock<IRepository<Car>>();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(x => x.CarRepo).Returns(repoMock.Object);
            repoMock.Setup(x => x.Query()).Returns(() =>
            {
                var c1 = new Car() { Id = 1, Model = "C1" };
                c1.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-5), Car = c1, Customer = new Customer() });

                var c2 = new Car() { Id = 2, Model = "C2" };
                c2.Rents.Add(new Rent()
                {
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now,
                    Car = c2,
                    Customer = new Customer()
                });

                var c3 = new Car() { Id = 3, Model = "C3" };
                c3.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-2), Car = c3, Customer = new Customer() });

                return new[] { c1, c2, c3 }.AsQueryable();
            });
            var carService = new CarService(uowMock.Object);


            var result = carService.GetAllAvailableCars();

            result.Should().HaveCount(1).And.AllSatisfy(x => x.Id.Should().Be(2));
        }
    }

    class TestUnitOfWork : IUnitOfWork
    {
        public IRepository<Car> CarRepo => new TestCarRepo();

        public IRepository<Rent> RentRepo => throw new NotImplementedException();

        public IRepository<Customer> CustomerRepo => throw new NotImplementedException();

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    class TestCarRepo : IRepository<Car>
    {
        public void Add(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public Car? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Car> Query()
        {
            var c1 = new Car() { Id = 1, Model = "C1" };
            c1.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-5), Car = c1, Customer = new Customer() });

            var c2 = new Car() { Id = 2, Model = "C2" };
            c2.Rents.Add(new Rent()
            {
                StartDate = DateTime.Now.AddDays(-5),
                EndDate = DateTime.Now,
                Car = c2,
                Customer = new Customer()
            });

            var c3 = new Car() { Id = 3, Model = "C3" };
            c3.Rents.Add(new Rent() { StartDate = DateTime.Now.AddDays(-2), Car = c3, Customer = new Customer() });

            return new[] { c1, c2, c3 }.AsQueryable();
        }

        public void Update(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}