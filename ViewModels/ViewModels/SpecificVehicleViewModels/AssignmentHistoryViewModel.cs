using Models.ModelFirebase;
using Models.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;
using ViewModels.Commands;
using ViewModels.State.Data;
using ViewModels.State.Navigators;

namespace ViewModels.VehicleViewModel
{
    public class AssignmentHistoryViewModel : ViewModelBase
    {
        private readonly IDataStore _dataStore;

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

        private readonly IStoringDataManagementService _storingDataManagementService;
        private IVehicleDataFirebase vehicle => _dataStore.GetVehicle();

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


        public AssignmentHistoryViewModel(IDataStore dataStore, IStoringDataManagementService storingDataManagementService, INavigator navigator)
        {
            _dataStore = dataStore;
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;
            _dataStore.StateChanged += DataStore_StateChanged;
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            NavigateToTripInfoCommand = new RelayCommand<TripFirebase>((p) =>
            {
                if (p == null) return;
                _dataStore.CurrentObject = p;
                Navigator.NavigateSwitch(Navigation, ViewType.TripInfo);
            });
            TrailerNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteVehicleNavigateCommand(p));
            DriverNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteDriverNavigateCommand(p)); ;
        }

        private void DataStore_StateChanged()
        {
            OnPropertyChanged(nameof(vehicle));
        }
        public override void Dispose()
        {
            _dataStore.StateChanged -= DataStore_StateChanged;

            base.Dispose();
        }


        #region Commands
        public ICommand LoadCommand { get; }
        public ICommand NavigateToTripInfoCommand { get; }
        public ICommand TrailerNavigateCommand { get; }
        public ICommand DriverNavigateCommand { get; }
        private async Task ExecuteLoadCommand()
        {
            IReadOnlyCollection<TripFirebase> temp = await _storingDataManagementService.WhereEqualToTrip("Vehicle.Id", vehicle.Id);
            if (temp != null && temp.Count != 0)
            {
                ListTrips = new ObservableCollection<TripFirebase>(temp);
            }
            TripsCollection = CollectionViewSource.GetDefaultView(ListTrips);
            TripsCollection.Filter = Filter;
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

        private async Task ExecuteVehicleNavigateCommand(object p)
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
