using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Navigators;
using ViewModels;
using Google.Apis.Discovery;

namespace ViewModels.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<DashboardViewModel> _createDashboardViewModel;
        private readonly CreateViewModel<AddDriverViewModel> _createAddDriverViewModel;
        private readonly CreateViewModel<AddVehicleViewModel> _createAddVehicleViewModel;
        private readonly CreateViewModel<VehicleAssignmentViewModel> _createVehicleAssignmentViewModel;
        private readonly CreateViewModel<VehicleListViewModel> _createVehicleListViewModel;
        private readonly CreateViewModel<DriverListViewModel> _createDriverListViewModel;
        private readonly CreateViewModel<MainDriverOverviewViewModel> _createMainDriverOverviewViewModel;
        private readonly CreateViewModel<MainVehicleOverviewViewModel> _createMainVehicleOverviewViewModel;
        private readonly CreateViewModel<RemindersListViewModel> _createRemindersListViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;

        public ViewModelFactory(CreateViewModel<DashboardViewModel> createDashboardViewModel,
                                CreateViewModel<AddDriverViewModel> createAddDriverViewModel,
                                CreateViewModel<AddVehicleViewModel> createAddVehicleViewModel,
                                CreateViewModel<VehicleAssignmentViewModel> createVehicleAssignmentViewModel,
                                CreateViewModel<VehicleListViewModel> createVehicleListViewModel,
                                CreateViewModel<DriverListViewModel> createDriverListViewModel,
                                CreateViewModel<MainDriverOverviewViewModel> createMainDriverOverviewViewModel,
                                CreateViewModel<MainVehicleOverviewViewModel> createMainVehicleOverviewViewModel,
                                CreateViewModel<RemindersListViewModel> createRemindersListViewModel,
                                CreateViewModel<LoginViewModel> createLoginViewModel)
        {
            _createDashboardViewModel = createDashboardViewModel;
            _createAddDriverViewModel = createAddDriverViewModel;
            _createAddVehicleViewModel = createAddVehicleViewModel;
            _createVehicleAssignmentViewModel = createVehicleAssignmentViewModel;
            _createVehicleListViewModel = createVehicleListViewModel;
            _createDriverListViewModel = createDriverListViewModel;
            _createMainDriverOverviewViewModel = createMainDriverOverviewViewModel;
            _createMainVehicleOverviewViewModel = createMainVehicleOverviewViewModel;
            _createRemindersListViewModel = createRemindersListViewModel;
            _createLoginViewModel = createLoginViewModel;
        }
        public ViewModelBase CreateViewModel(ViewType viewType)
        {

            switch (viewType)
            {
                case ViewType.Home:
                    return _createDashboardViewModel();
                case ViewType.VehicleList:
                    return _createVehicleListViewModel();
                case ViewType.DriverList:
                    return _createDriverListViewModel();
                case ViewType.VehicleAssignment:
                    return _createVehicleAssignmentViewModel();
                case ViewType.Reminders:
                    return _createRemindersListViewModel();
                case ViewType.AddDriver:
                    return _createAddDriverViewModel();
                case ViewType.AddVehicle:
                    return _createAddVehicleViewModel();
                case ViewType.DriverMainOverview:
                    return _createMainDriverOverviewViewModel();
                case ViewType.VehicleMainOverview:
                    return _createMainVehicleOverviewViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }

        }
    }
}
