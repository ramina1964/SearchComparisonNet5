using Microsoft.Extensions.DependencyInjection;
using SearchComparisonNet5.GUI.ViewModels;
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
            Services = new ServiceCollection();
            ConfigureServices(Services);
            _serviceProvider = Services.BuildServiceProvider();
        }

        public ServiceCollection Services { get; set; }

        public IServiceCollection LinearSearch { get; set; }

        public IServiceCollection BinarySearch { get; set; }

        public IServiceCollection DataGenerator { get; set; }

        private void ConfigureServices(IServiceCollection services)
        {
            _ = Services.AddSingleton<DataParameters>();
            _ = Services.AddSingleton<IDataGenerator, DataGenerator>();
            _ = Services.AddSingleton<LinearSearch>();
            _ = Services.AddSingleton<BinarySearch>();
            _ = Services.AddSingleton<MainViewModel>();
            _ = Services.AddSingleton<MainView>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainView = _serviceProvider.GetService<MainView>();
            mainView.Show();
        }

        private readonly IServiceProvider _serviceProvider;

    }
}
