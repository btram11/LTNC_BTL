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
        private List<IVehicleDataFirebase> _listVehicles = new List<IVehicleDataFirebase>();
        public List<IVehicleDataFirebase> ListVehicles
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
            }
        }

        public VehicleListViewModel(INavigator navigator, IStoringDataManagementService storingDataManagementService)
        {
            Navigation = navigator;
            _storingDataManagementService = storingDataManagementService;
            VehiclesCollection = CollectionViewSource.GetDefaultView(ListVehicles);

            UpdateViewModelCommand = new RelayCommand<ViewType>((p) => { return true; }, (p) =>
            {
                Navigator.NavigateSwitch(Navigation, p);
            });
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            VehiclesCollection.Filter = Filter;
        }

        private bool Filter(object ve)
        {
            IVehicleDataFirebase vehicle = ve as IVehicleDataFirebase;
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
            ListVehicles = await _storingDataManagementService.GetAllVehicles();
            VehiclesCollection = CollectionViewSource.GetDefaultView(ListVehicles);
        }

        public ICommand UpdateViewModelCommand { get; }
        public ICommand LoadCommand { get; }
    }
}
