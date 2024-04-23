//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;

//namespace ViewModels
//{
//    public class MenuLeftBarViewModel : ViewModelBase
//    {
//        #region Command Properties
//        public ICommand HomeViewCommand { get; }
//        public ICommand VehicleListViewCommand { get; }
//        public ICommand DriverListViewCommand { get; }
//        public ICommand VehicleAssignmentViewCommand { get; }
//        public ICommand RemindersListViewCommand { get; }
//        #endregion
//        public MenuLeftBarViewModel()
//        {
//            HomeViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
//            {
//                Window window = Window.GetWindow(p);
//                var viewModel = (MainViewModel)window.DataContext;
//                viewModel.CurrentViewModel = new DashboardViewModel();
//            });

//            VehicleListViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
//            {
//                Window window = Window.GetWindow(p);
//                var viewModel = (MainViewModel)window.DataContext;
//                viewModel.CurrentViewModel = new VehicleListViewModel();
//            });

//            VehicleAssignmentViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
//            {
//                Window window = Window.GetWindow(p);
//                var viewModel = (MainViewModel)window.DataContext;
//                viewModel.CurrentViewModel = new VehicleAssignmentViewModel();
//            });

//            RemindersListViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
//            {
//                Window window = Window.GetWindow(p);
//                var viewModel = (MainViewModel)window.DataContext;
//                viewModel.CurrentViewModel = new RemindersListViewModel();
//            });

//            DriverListViewCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
//            {
//                Window window = Window.GetWindow(p);
//                var viewModel = (MainViewModel)window.DataContext;
//                viewModel.CurrentViewModel = new DriverListViewModel();
//            });
//        }
//    }
//}
