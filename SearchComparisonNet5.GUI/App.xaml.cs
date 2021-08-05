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
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddSingleton<DataParameters>();
            _ = services.AddSingleton<IDataGenerator, DataGenerator>();
            _ = services.AddSingleton<LinearSearch>();
            _ = services.AddSingleton<BinarySearch>();
            _ = services.AddSingleton<MainViewModel>();
            _ = services.AddSingleton<MainView>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainView = _serviceProvider.GetService<MainView>();
            mainView.Show();
        }

        private readonly IServiceProvider _serviceProvider;

        public IServiceCollection LinearSearch { get; set; }

        public IServiceCollection BinarySearch { get; set; }

        public IServiceCollection DataGenerator { get; set; }
    }
}
