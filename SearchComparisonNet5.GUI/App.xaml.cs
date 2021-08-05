using Microsoft.Extensions.DependencyInjection;
using SearchComparisonNet5.GUI.Views;
using SearchComparisonNet5.Kernel.Interfaces;
using SearchComparisonNet5.Kernel.Models;
using System;
using System.Windows;


namespace SearchComparisonNet5.GUI
{
    public partial class App
    {
        public App()
        {
            var services = new ServiceCollection();
            //ConfigureServices(services);
            //_serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();

            //services.AddSingleton<IDataGenerator, DataGenerator>();
            //services.AddSingleton<IDataGenerator, DataGenerator>();
            //services.AddSingleton<IDataGenerator, DataGenerator>();
            //services.AddSingleton<IDataGenerator, DataGenerator>();
            //services.AddSingleton<IDataGenerator, DataGenerator>();


        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            MainView mainView = new();
            mainView.Show();
        }

        private readonly IServiceProvider _serviceProvider;

    }
}
