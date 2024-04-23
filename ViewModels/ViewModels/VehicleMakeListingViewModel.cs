using Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class VehicleMakeListingViewModel
    {
        private readonly IDataFromNHTSAService _dataFromNHTSAService;
        public ObservableCollection<string> VehicleMakeList { get; set; }
        public VehicleMakeListingViewModel(IDataFromNHTSAService dataFromNHTSAService)
        {
            _dataFromNHTSAService = dataFromNHTSAService;
        }

        public static VehicleMakeListingViewModel LoadVehicleMakeListingViewModel(IDataFromNHTSAService dataFromNHTSAService)
        {
            VehicleMakeListingViewModel vehicleMakeListingViewModel = new VehicleMakeListingViewModel(dataFromNHTSAService);
            vehicleMakeListingViewModel.LoadVehicleMakeListing();
            return vehicleMakeListingViewModel;
        }

        public void LoadVehicleMakeListing()
        {
            _dataFromNHTSAService.GetAllMakesListFromNHTSA().ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    VehicleMakeList = task.Result;
                }
            });
        }
    }
}
