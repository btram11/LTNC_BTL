using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.ViewModels;

namespace ViewModels.State.Navigators
{
    public class Navigator : ViewModelBase, INavigator
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private ViewModelBase _currentViewModel;
        private ViewType _typeTab;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));

            }
        }

        public ViewType TypeTab
        {
            get => _typeTab;
            set
            {
                _typeTab = value;
                OnPropertyChanged(nameof(TypeTab));
            }
        }
        public Navigator(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }

        public ViewModelBase NavigateToTab<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            return viewModel;
        }

        public static void NavigateSwitch(INavigator navigator, object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType curType = (ViewType)parameter;
                switch (curType)
                {
                    case ViewType.Home:
                        navigator.TypeTab = curType;
                        navigator.NavigateTo<DashboardViewModel>();
                        break;
                    case ViewType.VehicleList:
                        navigator.TypeTab = curType;
                        navigator.NavigateTo<VehicleListViewModel>();
                        break;
                    case ViewType.DriverList:
                        navigator.TypeTab = curType;
                        navigator.NavigateTo<DriverListViewModel>();
                        break;
                    case ViewType.VehicleAssignment:
                        navigator.TypeTab = ViewType.VehicleAssignment;
                        navigator.NavigateTo<VehicleAssignmentViewModel>();
                        break;
                    case ViewType.Reminders:
                        navigator.NavigateTo<RemindersListViewModel>();
                        break;
                    case ViewType.AddDriver:
                        navigator.TypeTab = ViewType.DriverList;
                        navigator.NavigateTo<AddDriverViewModel>();
                        break;
                    case ViewType.AddVehicle:
                        navigator.TypeTab = ViewType.VehicleList;
                        navigator.NavigateTo<AddVehicleViewModel>();
                        break;
                    case ViewType.DriverMainOverview:
                        navigator.TypeTab = ViewType.DriverList;
                        navigator.NavigateTo<MainDriverOverviewViewModel>();
                        break;
                    case ViewType.VehicleMainOverview:
                        navigator.TypeTab = ViewType.VehicleList;
                        navigator.NavigateTo<MainVehicleOverviewViewModel>();
                        break;
                    case ViewType.Login:
                        navigator.TypeTab = curType;
                        navigator.NavigateTo<LoginViewModel>();
                        break;
                    case ViewType.Register:
                        navigator.TypeTab = curType;
                        navigator.NavigateTo<SignUpViewModel>();
                        break;
                    case ViewType.TripInfo:
                        navigator.TypeTab = ViewType.TripList;
                        navigator.NavigateTo<TripInformationViewModel>();
                        break;
                    case ViewType.TripList:
                        navigator.TypeTab = ViewType.TripList;
                        navigator.NavigateTo<AssignmentListViewModel>();
                        break;
                }
            }
        }
    }
}
