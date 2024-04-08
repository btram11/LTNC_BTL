using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModels
{
    public class MenuLeftBarViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public ViewModelBase CurrentViewModel { get; private set; }

        #region Command Properties
        public ICommand HomeViewCommand { get; set; }
        public ICommand VehicleListViewCommand { get; set; }
        #endregion
        public MenuLeftBarViewModel()
        {
            HomeViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                //FrameworkElement window = GetWindowParent(p);
                Window window = Window.GetWindow(p);
                //var w = window as Window;
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.currentViewModel = new DashboardViewModel();
            });

            VehicleListViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Window window = Window.GetWindow(p);
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.currentViewModel = new VehicleListViewModel();
            });
        }

        private FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
