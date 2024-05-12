using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ViewModels.State.Navigators;
using System.Collections.ObjectModel;
using Models.ModelFirebase;
using System.ComponentModel;
using ViewModels.Commands;
using Models.Services.Firebase;
using System.Windows.Data;
using Models.Services;
using API.Services;
using System.Net.Http;
using ViewModels.State.Data;

namespace ViewModels
{
    public class VehicleListViewModel : ViewModelBase
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
        private readonly IDataFromNHTSAService _dataFromNHTSAService;
        private IDataStore _dataStore;

        private bool _IsLoaded { get; set; } = false;
        public ObservableCollection<string> VehicleTypeList { get; private set; }
        public ObservableCollection<object> StatusList { get; private set; }

        private ObservableCollection<IVehicleDataFirebase> _listVehicles = new ObservableCollection<IVehicleDataFirebase>();
        public ObservableCollection<IVehicleDataFirebase> ListVehicles
        {
            get => _listVehicles;
            set
            {
                _listVehicles = value;
                OnPropertyChanged(nameof(ListVehicles));
            }
        }

        private ICollectionView _vehiclesCollection;
        public ICollectionView VehiclesCollection
        {
            get => _vehiclesCollection;
            set
            {
                _vehiclesCollection = value;
                OnPropertyChanged(nameof(VehiclesCollection));
                _vehiclesCollection.Refresh();
            }
        }

        private string _name;
        private string _vehicleType;
        private object _status;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                _vehiclesCollection.Refresh();
            }
        }
        public string VehicleTypeFilter
        {
            get => _vehicleType;
            set
            {
                _vehicleType = value;
                OnPropertyChanged(nameof(VehicleTypeFilter));
                _vehiclesCollection.Refresh();
            }
        }
        public object StatusFilter
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(StatusFilter));
                _vehiclesCollection.Refresh();
            }
        }

        public VehicleListViewModel(INavigator navigator, IStoringDataManagementService storingDataManagementService, IDataFromNHTSAService dataFromNHTSAService, IDataStore dataStore)
        {
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;
            _dataFromNHTSAService = dataFromNHTSAService;
            _dataStore = dataStore;
            //VehiclesCollection = CollectionViewSource.GetDefaultView(ListVehicles);
            StatusList = new ObservableCollection<object>(Enum.GetValues(typeof(VehicleStatus)).Cast<object>().ToList());
            StatusList.Insert(0, "(None)");

            UpdateViewModelCommand = new RelayCommand<ViewType>((p) => Navigator.NavigateSwitch(Navigation, p));

            DriverNavigateCommand = new AsyncRelayCommand<object>((p) => ExecuteDriverNavigateCommand(p));

            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            NavigateToMainVehicleOverviewCommand = new RelayCommand<IVehicleDataFirebase>((vehicle) => { 
                _dataStore.CurrentObject = vehicle;
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleMainOverview);
            });

        }

        private bool Filter(object ve)
        {
            IVehicleDataFirebase vehicle = ve as IVehicleDataFirebase;
            bool IsTrue = true;
            if (!string.IsNullOrEmpty(VehicleTypeFilter) && VehicleTypeFilter != "(None)")
            {
                IsTrue = IsTrue && vehicle.VehicleType.Contains(VehicleTypeFilter);
            }
            if (StatusFilter != null && StatusFilter.ToString() != "(None)" && !string.IsNullOrEmpty(StatusFilter.ToString()))
            {
                IsTrue = IsTrue && vehicle.VehicleStatus == (VehicleStatus)StatusFilter;
            }
            if (!string.IsNullOrEmpty(Name))
            {
                IsTrue = IsTrue && vehicle.Name.Contains(Name);
                IsTrue = IsTrue && vehicle.VIN.Contains(Name);
            }
            return IsTrue;
            //you can write logic for filter here
            //if (!string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(DepartmentName))
            //    return vehicle.Department.Contains(DepartmentName) && employee.EmployeeName.Contains(EmployeeName);
            //else if (string.IsNullOrEmpty(EmployeeName))
            //    return employee.Department.Contains(DepartmentName);
            //else
            //    return employee.EmployeeName.Contains(EmployeeName);
        }


        #region Commands
        public ICommand UpdateViewModelCommand { get; }
        public ICommand DriverNavigateCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand NavigateToMainVehicleOverviewCommand { get; }


        private async Task ExecuteLoadCommand()
        {
            try
            {
                //ClearingAllInputField();
                if (_IsLoaded == false)
                {
                    VehicleTypeList = await _dataFromNHTSAService.GetDataListByNameFromNHTSA("Vehicle Type");
                    VehicleTypeList.Insert(0, "(None)");
                    OnPropertyChanged(nameof(VehicleTypeList));

                    List<IVehicleDataFirebase> temp = await _storingDataManagementService.GetAllVehicles();
                    if (temp != null)
                    {
                        ListVehicles = new ObservableCollection<IVehicleDataFirebase>(temp);
                    }
                    VehiclesCollection = CollectionViewSource.GetDefaultView(ListVehicles);
                    VehiclesCollection.Filter = Filter;
                    _IsLoaded = true;
                }



            }
            catch (HttpRequestException ex)
            {
                MessageBoxResult result = MessageBox.Show("Please check your internet and try again", "", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                {
                    Navigator.NavigateSwitch(Navigation, ViewType.VehicleList);
                }
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
                Navigator.NavigateSwitch(Navigation, ViewType.VehicleList);
            }
        }
        #endregion
    }
}
