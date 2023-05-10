using Autofac;
using ppedv.CyanBayCars.Core;
using ppedv.CyanBayCars.Data.EfCore;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Hello, World!");

string conString = "Server=(localdb)\\mssqllocaldb;Database=CyanBayCars_Dev;Trusted_Connection=true;";

//DI per code
//var repo = new EfRepository(conString);
//var core = new CarService(repo);

//DI per Reflection (ohne Referenz)
//var path = @"C:\Users\ar2\source\repos\ppedvAG\InHouse_Architektur\ppedv.CyanBayCars\ppedv.CyanBayCars.Data.EfCore\bin\Debug\net7.0\ppedv.CyanBayCars.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(path);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = (IRepository)Activator.CreateInstance(typeMitRepo, conString);
//var core = new CarService(repo);

//DI per AutoFac
var builder = new ContainerBuilder();
builder.RegisterType<EfUnitOfWork>().AsImplementedInterfaces()
                                    .WithParameter("conString", conString)
                                    .SingleInstance();
builder.RegisterType<CarService>().AsImplementedInterfaces();
builder.RegisterType<RentService>().AsImplementedInterfaces();

var container = builder.Build();

var carService = container.Resolve<ICarService>();
var rentService = container.Resolve<IRentService>();

var uow = container.Resolve<IUnitOfWork>();
var specialCars = uow.CarRepo.GetAllCarsThatHaveSpecialNeeds();



foreach (var car in carService.GetAllAvailableCars())
{
    Console.WriteLine($"{car.Manufacturer} {car.Model}");
}

Debugger.Break();

var toRent = carService.GetAllAvailableCars().FirstOrDefault();
var cust = new Customer() { Name = "Fred" };
var newRent = new Rent() { Car = toRent, Customer = cust, StartKm = 8723 };
rentService.StartRent(newRent);

