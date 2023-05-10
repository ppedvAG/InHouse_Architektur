using Microsoft.Extensions.DependencyInjection;
using ppedv.CyanBayCars.Data.EfCore;
using ppedv.CyanBayCars.Models.Contracts;
using ppedv.CyanBayCars.UI.WPF.ViewModels;
using System;
using System.Windows;

namespace ppedv.CyanBayCars.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var conString = "Server=(localdb)\\mssqllocaldb;Database=CyanBayCars_Dev;Trusted_Connection=true;";
            services.AddSingleton<IUnitOfWork, EfUnitOfWork>(x => new EfUnitOfWork(conString));
            services.AddSingleton<CarViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
