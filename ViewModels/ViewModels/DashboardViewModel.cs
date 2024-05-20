using Models.Services;
using Models.Services.Firebase;
using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.State.Data;
using ViewModels.State.Navigators;
using Models.Vehicles;

namespace ViewModels
{
    public class DashboardViewModel: ViewModelBase
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

        private readonly IFuelPriceService _fuelPriceService;
        private readonly IStoringDataManagementService _storingDataManagementService;
        private readonly IDataStore _dataStore;

        public ObservableCollection<string> nameGasPrice { get; set; } = new ObservableCollection<string>();
        public Dictionary<string, string> keyValuePairs { get; set; } = new Dictionary<string, string>();

        public DashboardViewModel(INavigator navigator, IFuelPriceService fuelPriceService, IDataStore dataStore, IStoringDataManagementService storingDataManagementService)
        {
            Navigation = navigator;
            _fuelPriceService = fuelPriceService;
            _storingDataManagementService = storingDataManagementService;
            _dataStore = dataStore;
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
        }

        #region Commands
        public ICommand LoadCommand { get; }
        private async Task ExecuteLoadCommand()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = Cursors.Wait;
            });
            try
            {
                var GasPriceTask = _fuelPriceService.GetPrice();
                var GetAllVehicle = _storingDataManagementService.GetAllVehicles();
                var GetAllDriverTask = _storingDataManagementService.GetAllDrivers();
                var GetAllTripTask = _storingDataManagementService.GetAllTrips();
                await Task.WhenAll(GasPriceTask,  GetAllVehicle, GetAllDriverTask, GetAllTripTask);

                nameGasPrice = GasPriceTask.Result;
                List<IVehicleDataFirebase> vehicleData = GetAllVehicle.Result;
                IReadOnlyCollection<DriverFirebase> drivers = GetAllDriverTask.Result;
                IReadOnlyCollection<TripFirebase> trips = GetAllTripTask.Result;

                putKeyPairsDriver(drivers);
                putKeyPairsVehicle(vehicleData);
                putKeyPairsTrip(trips);



                _dataStore.FuelPrice = nameGasPrice;
                OnPropertyChanged(nameof(nameGasPrice));
                OnPropertyChanged(nameof(keyValuePairs));
                
            }
            catch (HttpRequestException )
            {
                MessageBox.Show("Please check your internet again and connect again or keep using offline", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } 
            finally
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = null;
                });
            }
        }

        private void putKeyPairsTrip(IReadOnlyCollection<TripFirebase> trips)
        {
            if (trips == null) return;
            int TotalTrips = trips.Count();
            keyValuePairs[nameof(TotalTrips)] = TotalTrips.ToString();
        }

        private void putKeyPairsVehicle(List<IVehicleDataFirebase> vehicleData)
        {
            if (vehicleData == null) return;
            int TotalVehicles = vehicleData.Count(obj => obj.VehicleType != "Trailer");
            int TotalTrailers = vehicleData.Count(obj => obj.VehicleType == "Trailer");
            keyValuePairs[nameof(TotalVehicles)] = TotalVehicles.ToString();
            keyValuePairs[nameof(TotalTrailers)] = TotalTrailers.ToString();
            int Available = vehicleData.Count(obj => obj.VehicleStatus == VehicleStatus.Available);
            int InUse = vehicleData.Count(obj => obj.VehicleStatus == VehicleStatus.InUse);
            int Repairing = vehicleData.Count(obj => obj.VehicleStatus == VehicleStatus.Repairing);
            keyValuePairs[nameof(Available)] = Available.ToString();
            keyValuePairs[nameof(InUse)] = InUse.ToString();
            keyValuePairs[nameof(Repairing)] = Repairing.ToString();
        }

        private void putKeyPairsDriver(IReadOnlyCollection<DriverFirebase> drivers)
        {
            if (drivers == null) return;
            int Available = drivers.Count(obj => obj.Status == DriverStatus.Available);
            int OnDuty = drivers.Count(obj => obj.Status == DriverStatus.OnDuty);
            int Vaction = drivers.Count(obj => obj.Status == DriverStatus.Vacation);
            int TotalDrivers = drivers.Count();
            keyValuePairs[nameof(TotalDrivers)] = TotalDrivers.ToString();
            keyValuePairs["Driver.Available"] =  Available.ToString();
            keyValuePairs[nameof(Vaction)] = Vaction.ToString();
            keyValuePairs[nameof(OnDuty)] = OnDuty.ToString();
        }

        #endregion
        ~DashboardViewModel()
        {

        }
    }
}
