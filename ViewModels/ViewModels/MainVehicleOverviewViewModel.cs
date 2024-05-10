using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.VehicleViewModel;

namespace ViewModels
{
    public class MainVehicleOverviewViewModel : ViewModelBase
    {
        private ViewModelBase _tabView;

        public ViewModelBase TabView
        {
            get { return _tabView; }
            set
            {
                _tabView = value;
                OnPropertyChanged(nameof(TabView));
            }
        }
        public ICommand OverviewTabCommand { get; }
        public ICommand ServiceHistoryTabCommand { get; }
        public ICommand SpecsTabCommand { get; }
        public ICommand AssignmentHistoryTabCommand { get; }


        public MainVehicleOverviewViewModel()
        {
            TabView = new OverviewVehicleViewModel();

            OverviewTabCommand = new RelayCommand<UserControl>((p) =>
            {
                TabView = new OverviewVehicleViewModel();
            });

            ServiceHistoryTabCommand = new RelayCommand<UserControl>((p) =>
            {

                //MainVehicleOverviewViewModel cur = (MainVehicleOverviewViewModel)p.DataContext;
                TabView = new ServiceHistoryViewModel();
            });

            SpecsTabCommand = new RelayCommand<UserControl>((p) =>
            {
                TabView = new SpecsViewModel();
            });

            AssignmentHistoryTabCommand = new RelayCommand<UserControl>((p) =>
            {
                TabView = new AssignmentHistoryViewModel();
            });
        }

        ~MainVehicleOverviewViewModel()
        {

        }
    }
}
