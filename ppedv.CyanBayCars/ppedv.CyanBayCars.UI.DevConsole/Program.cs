using Autofac;
using ppedv.CyanBayCars.Core;
using ppedv.CyanBayCars.Data.EfCore;
using ppedv.CyanBayCars.Models.Contracts;
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
builder.RegisterType<EfRepository>().AsImplementedInterfaces()
                                    .WithParameter("conString", conString)
                                    .SingleInstance();
var container = builder.Build();
var core = new CarService(container.Resolve<IRepository>());

foreach (var car in core.GetAllAvailableCars())
{
    Console.WriteLine($"{car.Manufacturer} {car.Model}");
}