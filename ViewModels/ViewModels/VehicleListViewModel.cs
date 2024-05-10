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

        private readonly IStoringDataManagementService _storingDataManagementService;
        private readonly IDataFromNHTSAService _dataFromNHTSAService;

        private bool _IsLoaded { get; set; } = false;
        public ObservableCollection<string> VehicleTypeList { get; private set; }

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

        private string _vehicleType;
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

        public VehicleListViewModel(INavigator navigator, IStoringDataManagementService storingDataManagementService, IDataFromNHTSAService dataFromNHTSAService)
        {
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;
            _dataFromNHTSAService = dataFromNHTSAService;
            //VehiclesCollection = CollectionViewSource.GetDefaultView(ListVehicles);

            UpdateViewModelCommand = new RelayCommand<ViewType>((p) => Navigator.NavigateSwitch(Navigation, p));
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            
        }

        private bool Filter(object ve)
        {
            IVehicleDataFirebase vehicle = ve as IVehicleDataFirebase;
            if (!string.IsNullOrEmpty(VehicleTypeFilter) && VehicleTypeFilter != "(None)")
            {
                return vehicle.VehicleType.Contains(VehicleTypeFilter);
            }
            return true;
            //you can write logic for filter here
            //if (!string.IsNullOrEmpty(EmployeeName) && !string.IsNullOrEmpty(DepartmentName))
            //    return vehicle.Department.Contains(DepartmentName) && employee.EmployeeName.Contains(EmployeeName);
            //else if (string.IsNullOrEmpty(EmployeeName))
            //    return employee.Department.Contains(DepartmentName);
            //else
            //    return employee.EmployeeName.Contains(EmployeeName);
        }

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
            catch (Exception ex)
            {

            }


            
        }

        public ICommand UpdateViewModelCommand { get; }
        public ICommand LoadCommand { get; }
    }
}
