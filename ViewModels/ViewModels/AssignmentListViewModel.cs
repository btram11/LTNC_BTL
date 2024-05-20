using Models.ModelFirebase;
using Models.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using ViewModels.Commands;
using ViewModels.State.Navigators;
using ViewModels.State.Data;

namespace ViewModels
{
    public class AssignmentListViewModel : ViewModelBase
    {
        private readonly IDataStore _dataStore;

        public INavigator Navigation { get; }

        private readonly IStoringDataManagementService _storingDataManagementService;

        public ObservableCollection<string> TransportationTypeList { get; private set; } = new ObservableCollection<string> { "Passenger", "Cargo" };
        public ObservableCollection<object> StatusList { get; private set; }

        private ObservableCollection<TripFirebase> _listTrips = new ObservableCollection<TripFirebase>();
        public ObservableCollection<TripFirebase> ListTrips
        {
            get => _listTrips;
            set
            {
                _listTrips = value;
                OnPropertyChanged(nameof(ListTrips));
            }
        }

        private ICollectionView _tripsCollection;
        public ICollectionView TripsCollection
        {
            get => _tripsCollection;
            set
            {
                _tripsCollection = value;
                OnPropertyChanged(nameof(TripsCollection));
                _tripsCollection.Refresh();
            }
        }




        private string _location;
        private string _transType;
        private string _status;

        public string LocationFilter
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(LocationFilter));
                _tripsCollection.Refresh();
            }
        }
        public string TransportationTypeFilter
        {
            get => _transType;
            set
            {
                _transType = value;
                OnPropertyChanged(nameof(TransportationTypeFilter));
                _tripsCollection.Refresh();
            }
        }
        public string StatusFilter
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(StatusFilter));
                _tripsCollection.Refresh();
            }
        }


        public AssignmentListViewModel(IDataStore dataStore, IStoringDataManagementService storingDataManagementService, INavigator navigator)
        {
            _dataStore = dataStore;
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            TrailerNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteTrailerNavigateCommand(p));
            DriverNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteDriverNavigateCommand(p));
            VehicleNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteVehicleNavigateCommand(p));
            NavigateToTripInfoCommand = new RelayCommand<TripFirebase>((p) =>
            {
                if (p == null) return;
                _dataStore.CurrentObject = p;
                Navigator.NavigateSwitch(Navigation, ViewType.TripInfo);
            });
        }
        

        #region Commands
        public ICommand LoadCommand { get; }
        
        public ICommand NavigateToTripInfoCommand { get; }
        public ICommand TrailerNavigateCommand { get; }
        public ICommand VehicleNavigateCommand { get; }
        public ICommand DriverNavigateCommand { get; }
        private async Task ExecuteLoadCommand()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = Cursors.Wait;
            });
            IReadOnlyCollection<TripFirebase> temp = await _storingDataManagementService.GetAllTrips();
            if (temp != null && temp.Count != 0)
            {
                ListTrips = new ObservableCollection<TripFirebase>(temp);
            }
            else ListTrips = new ObservableCollection<TripFirebase>();
            TripsCollection = CollectionViewSource.GetDefaultView(ListTrips);
            TripsCollection.Filter = Filter;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = null;
            });
        }

        private bool Filter(object obj)
        {
            TripFirebase trip = obj as TripFirebase;
            bool IsTrue = true;
            if (!string.IsNullOrEmpty(TransportationTypeFilter))
            {
                IsTrue = IsTrue && trip.TransportationType == TransportationTypeFilter;
            }
            if (StatusFilter != null && !string.IsNullOrEmpty(StatusFilter.ToString()))
            {
                IsTrue = IsTrue && trip.Status == StatusFilter;
            }
            if (!string.IsNullOrEmpty(LocationFilter))
            {
                IsTrue = IsTrue && (trip.Origin.Contains(LocationFilter) || trip.Destination.Contains(LocationFilter));
            }
            return IsTrue;
        }

        private async Task ExecuteTrailerNavigateCommand(object p)
        {
            try
            {
                if (p == null) return;
                string id = p as string;
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleMainOverview);

                TrailerFirebase temp = await _storingDataManagementService.GetVehicleById<TrailerFirebase>(id);
                _dataStore.CurrentObject = temp;
            }
            catch (Exception)
            {
                MessageBox.Show("There is an error in loading data. We will direct you back to previous page");
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleMainOverview);
            }
        }

        private async Task ExecuteVehicleNavigateCommand(object p)
        {
            try
            {
                if (p == null) return;
                string id = p as string;
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleMainOverview);

                VehicleFirebase temp = await _storingDataManagementService.GetVehicleById<VehicleFirebase>(id);
                _dataStore.CurrentObject = temp;
            }
            catch (Exception)
            {
                MessageBox.Show("There is an error in loading data. We will direct you back to previous page");
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleMainOverview);
            }
        }

        private async Task ExecuteDriverNavigateCommand(object p)
        {
            try
            {
                if (p == null) return;
                string id = p as string;
                Navigator.NavigateSwitch(Navigation, ViewType.DriverMainOverview);

                DriverFirebase temp = await _storingDataManagementService.GetDriverById(id);
                _dataStore.CurrentObject = temp;
            }
            catch (Exception)
            {
                MessageBox.Show("There is an error in loading data. We will direct you back to previous page");
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleMainOverview);
            }
        }
        #endregion
    }
}
