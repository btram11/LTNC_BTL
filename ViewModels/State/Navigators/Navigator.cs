using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels.State.Navigators
{
    public class Navigator : ViewModelBase, INavigator
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private ViewModelBase _currentViewModel;
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
        public Navigator(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }

        public static void NavigateSwitch(INavigator navigator, object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType curType = (ViewType)parameter;
                switch (curType)
                {
                    case ViewType.Home:
                        navigator.NavigateTo<DashboardViewModel>();
                        break;
                    case ViewType.VehicleList:
                        navigator.NavigateTo<VehicleListViewModel>();
                        break;
                    case ViewType.DriverList:
                        navigator.NavigateTo<DriverListViewModel>();
                        break;
                    case ViewType.VehicleAssignment:
                        navigator.NavigateTo<VehicleAssignmentViewModel>();
                        break;
                    case ViewType.Reminders:
                        navigator.NavigateTo<RemindersListViewModel>();
                        break;
                    case ViewType.AddDriver:
                        navigator.NavigateTo<AddDriverViewModel>();
                        break;
                    case ViewType.AddVehicle:
                        navigator.NavigateTo<AddVehicleViewModel>();
                        break;
                    case ViewType.DriverMainOverview:
                        navigator.NavigateTo<MainDriverOverviewViewModel>();
                        break;
                    case ViewType.VehicleMainOverview:
                        navigator.NavigateTo<MainVehicleOverviewViewModel>();
                        break;
                    case ViewType.Login:
                        navigator.NavigateTo<LoginViewModel>();
                        break;
                    case ViewType.Register:
                        navigator.NavigateTo<SignUpViewModel>();
                        break;
                }
            }
        }
    }
}
