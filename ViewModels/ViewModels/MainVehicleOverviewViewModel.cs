using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels.State.Navigators;
using ViewModels.State.Data;
namespace ViewModels
{
    public class MainVehicleOverviewViewModel : ViewModelBase
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

        private readonly IDataStore _dataStore;
        public IVehicleDataFirebase Vehicle
        {
            get
            {
                if (_dataStore.CurrentObject is IVehicleDataFirebase)
                    return(IVehicleDataFirebase) _dataStore.CurrentObject;
                return null;
            }
        }
        private object _tabView = "Overview";
        private string _tabName = "Overview";

        public object TabView
        {
            get { return _tabView; }
            set
            {
                _tabView = value;
                OnPropertyChanged(nameof(TabView));
            }
        }
        public string TabName 
        { 
            get => _tabName;
            set
            {
                _tabName = value;
                OnPropertyChanged(nameof(TabName));
            } 
        }


        public ICommand UpdateTabCommand { get; }

        public ICommand UpdateViewCommand { get; }

        public MainVehicleOverviewViewModel(INavigator navigator, IDataStore dataStore)
        {
            Navigation = navigator;
            _dataStore = dataStore;
            _dataStore.StateChanged += DataStore_StateChanged;
            UpdateTabCommand = new RelayCommand<string>((content) => ExecuteUpdateTabCommand(content));
            UpdateViewCommand = new RelayCommand((p) =>
            {
                Navigator.NavigateSwitch(navigator, p);
            });
        }

        private void DataStore_StateChanged()
        {
            OnPropertyChanged(nameof(Vehicle));
        }

        private void ExecuteUpdateTabCommand(string content)
        {
            switch (content)
            {
                case "Assignment History":
                    TabName = content;
                    TabView = Navigation.NavigateToTab<VehicleViewModel.AssignmentHistoryViewModel>();
                    return;
                //case "Service History":
                //    TabName = content;
                //    TabView = new VehicleViewModel.ServiceHistoryViewModel();
                //    return;
                case "Overview": case "Specs":
                    TabName = content;
                    TabView = content;
                    return;
                default:
                    TabName = "Overview";
                    TabView = "Overview";
                    return;
            }
        }


        public override void Dispose()
        {
            _dataStore.StateChanged -= DataStore_StateChanged;

            base.Dispose();
        }

        ~MainVehicleOverviewViewModel()
        {

        }
    }
}
