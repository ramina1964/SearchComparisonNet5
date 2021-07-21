using SearchComparisonNet5.GUI.ViewModels;
using SearchComparisonNet5.Kernel.Models;
using System.Windows;

namespace SearchComparisonNet5.GUI
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            var searchItem = new SearchItem();
            var vm = new MainViewModel(searchItem);
            DataContext = vm;
        }
    }
}
