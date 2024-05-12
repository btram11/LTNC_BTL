using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ViewModels.State.Navigators;
using System.Collections.ObjectModel;
using Models.ModelFirebase;
using System.ComponentModel;
using Models.Services.Firebase;
using ViewModels.Commands;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;
using ViewModels.State.Data;

namespace ViewModels
{
    public class DriverListViewModel : ViewModelBase
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

        private readonly IStoringDataManagementService _storingDataManagementService;
        private readonly IDataStore _dataStore;

        private bool _IsLoaded { get; set; } = false;

        public ObservableCollection<string> LicenseTypeList { get; private set; } = new ObservableCollection<string> { "B2", "C", "D", "E", "FC" };
        public ObservableCollection<string> StatusList { get; private set; }
        private ObservableCollection<DriverFirebase> _listDrivers = new ObservableCollection<DriverFirebase>();
        public ObservableCollection<DriverFirebase> ListDrivers
        {
            get => _listDrivers;
            set
            {
                _listDrivers = value;
                OnPropertyChanged(nameof(ListDrivers));
            }
        }

        private ICollectionView _driversCollection;
        public ICollectionView DriversCollection
        {
            get => _driversCollection;
            set
            {
                _driversCollection = value;
                OnPropertyChanged(nameof(DriversCollection));
                _driversCollection.Refresh();
            }
        }

        private string _driverName;
        private string _licenseType;
        private string _status;

        public string Name 
        {
            get => _driverName;
            set
            {
                _driverName = value;
                OnPropertyChanged(nameof(Name));
                _driversCollection.Refresh();
            }
        }
        public string LicenseTypeFilter
        {
            get => _licenseType;
            set
            {
                _licenseType = value;
                OnPropertyChanged(nameof(LicenseTypeFilter));
                _driversCollection.Refresh();
            }
        }
        public string StatusFilter 
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(StatusFilter));
                _driversCollection.Refresh();
            }
        }

        public DriverListViewModel(INavigator navigator, IStoringDataManagementService storingDataManagementService, IDataStore dataStore)
        {
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;
            _dataStore = dataStore;
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            UpdateViewCommand = new RelayCommand<ViewType>((p) =>
            {
                Navigator.NavigateSwitch(Navigation, p);
            });
            NavigateToMainDriverOverviewCommand = new RelayCommand<DriverFirebase>((driver) => {
                _dataStore.CurrentObject = driver;
                Navigator.NavigateSwitch(Navigation, ViewType.DriverMainOverview);
            });
            VehicleNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteVehicleNavigateCommand(p));
        }

        



        #region Commands
        public ICommand LoadCommand {  get; }
        public ICommand VehicleNavigateCommand { get; }
        public ICommand UpdateViewCommand { get; }
        public ICommand NavigateToMainDriverOverviewCommand { get; }

        private async Task ExecuteLoadCommand()
        {
            IReadOnlyCollection<DriverFirebase> temp = await _storingDataManagementService.GetAllDrivers();
            if (temp != null)
            {
                ListDrivers = new ObservableCollection<DriverFirebase>(temp);
            }
            DriversCollection = CollectionViewSource.GetDefaultView(ListDrivers);
            DriversCollection.Filter = Filter;

        }


        private bool Filter(object dr)
        {
            DriverFirebase driver = dr as DriverFirebase;
            bool IsTrue = true;
            if (!string.IsNullOrEmpty(LicenseTypeFilter) && LicenseTypeFilter != "(None)")
            {
                string[] driverClassess = driver.DrivingLicenseClass.Split(',').Select(p => p.Trim()).ToArray();

                bool HasFilterClass = driverClassess.Any(c => c == LicenseTypeFilter);
                IsTrue = IsTrue && HasFilterClass;
            }
            if (StatusFilter != null && StatusFilter.ToString() != "(None)" && !string.IsNullOrEmpty(StatusFilter.ToString()))
            {
                IsTrue = IsTrue && driver.Status.Contains(StatusFilter);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                string FullName = $"{driver.FirstName} {driver.LastName}";
                IsTrue = IsTrue && (driver.FirstName.Contains(Name) ||
                    driver.LastName.Contains(Name) || 
                    FullName.Contains(Name));
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

                VehicleFirebase temp = await _storingDataManagementService.GetVehicleById<VehicleFirebase>(id);
                _dataStore.CurrentObject = temp;
            }
            catch (Exception)
            {
                MessageBox.Show("There is an error in loading data. We will direct you back to previous page");
                Navigator.NavigateSwitch(Navigation, ViewType.DriverList);
            }
        }

        #endregion
    }
}