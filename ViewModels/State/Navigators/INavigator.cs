using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.State.Navigators
{
    public enum ViewType
    {
        Login,
        Register,
        Home,
        AddVehicle,
        VehicleList,
        VehicleMainOverview,
        AddDriver,
        DriverList,
        DriverMainOverview,
        VehicleAssignment,
        Reminders,
        TripInfo,
        TripList,
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
        ViewModelBase NavigateToTab<TViewModel>() where TViewModel : ViewModelBase;
    }
}
