using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    public class VehicleListViewModel: MainViewModel
    {
        public ICommand AddVehicleCommand { get; }
        public VehicleListViewModel()
        {
            AddVehicleCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Window window = Window.GetWindow(p);
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.CurrentViewModel = new AddVehicleViewModel();
            });
        }
    }
}
