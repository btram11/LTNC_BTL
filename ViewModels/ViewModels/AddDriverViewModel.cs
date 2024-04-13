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
    public class AddDriverViewModel : ViewModelBase
    {
        public AddDriverViewModel()
        {
            BackToDriverListView = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Window window = Window.GetWindow(p);
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.currentViewModel = new DriverListViewModel();
            });
        }

        public ICommand AddVehicleCommand { get; }
        public ICommand DecodeVinCommand { get; }
        public ICommand BackToDriverListView { get; }
    }
}
