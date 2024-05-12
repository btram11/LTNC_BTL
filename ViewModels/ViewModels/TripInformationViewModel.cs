using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Data;
using Models.Services.Firebase;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class TripInformationViewModel: ViewModelBase
    {
        private readonly IDataStore _dataStore;
        private readonly IStoringDataManagementService _storingDataManagementService;

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
        public TripFirebase Trip => _dataStore.CurrentObject as TripFirebase;
        public TrailerFirebase Trailer = null;
        public VehicleFirebase Vehicle = null;
        public DriverFirebase Driver = null;

        public TripInformationViewModel(IDataStore dataStore, IStoringDataManagementService storingDataManagementService, INavigator navigator)
        {
            _dataStore = dataStore;
            _storingDataManagementService = storingDataManagementService;
            Navigation = navigator;
            //LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
            UpdateViewCommand = new RelayCommand<ViewType>((p) =>
            {
                Navigator.NavigateSwitch(Navigation, p);
            });
        }
        public ICommand UpdateViewCommand { get; }

        //private async Task ExecuteLoadCommand()
        //{
        //    if (Trip ==  null) return;
        //    if (Trip.HasTrailer && Trip.Trailer != null)
        //    {
        //        Trailer = await _storingDataManagementService.GetVehicleById<TrailerFirebase>(Trip?.Trailer["Id"]?.ToString()); 
        //    }
        //}
        //public ICommand LoadCommand { get; }


    }
}
