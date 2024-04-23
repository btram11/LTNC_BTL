using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class VehicleListViewModel: ViewModelBase
    {
        private INavigator _navigation;
        public INavigator Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }
        public ICommand AddVehicleCommand { get; }
        public VehicleListViewModel(INavigator navigator)
        {
            Navigation = navigator;
            AddVehicleCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                //Window window = Window.GetWindow(p);
                //var viewModel = (MainViewModel)window.DataContext;
                //viewModel.CurrentViewModel = new AddVehicleViewModel();
                Navigation.NavigateTo<AddVehicleViewModel>();
            });
        }
    }
}
