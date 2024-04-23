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
        Home,
        AddVehicle,
        VehicleList,
        VehicleMainOverview,
        AddDriver,
        DriverList,
        DriverMainOverview,
        VehicleAssignment,
        Reminders
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    }
}
