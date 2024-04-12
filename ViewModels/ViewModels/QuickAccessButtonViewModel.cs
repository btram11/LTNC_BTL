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
    public class QuickAccessButtonViewModel: ViewModelBase
    {
        public ICommand OpenAddVehicleViewCommand { get; set; }
        public ICommand OpenAddDriverViewCommand { get; set; }
        public QuickAccessButtonViewModel()
        {
            OpenAddVehicleViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Window window = Window.GetWindow(p);
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.currentViewModel = new AddVehicleViewModel();
            });
        }
    }
}
