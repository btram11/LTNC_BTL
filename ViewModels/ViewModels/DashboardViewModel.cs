using Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.State.Data;
using ViewModels.State.Navigators;

namespace ViewModels
{
    public class DashboardViewModel: ViewModelBase
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

        private readonly IFuelPriceService _fuelPriceService;
        private readonly IDataStore _dataStore;

        public ObservableCollection<string> nameGasPrice { get; set; } = new ObservableCollection<string>();

        public DashboardViewModel(INavigator navigator, IFuelPriceService fuelPriceService, IDataStore dataStore)
        {
            Navigation = navigator;
            _fuelPriceService = fuelPriceService;
            _dataStore = dataStore;
            LoadCommand = new AsyncRelayCommand(ExecuteLoadCommand);
        }

        #region Commands
        public ICommand LoadCommand { get; }
        private async Task ExecuteLoadCommand()
        {
            try
            {
                nameGasPrice = await _fuelPriceService.GetPrice();
                _dataStore.FuelPrice = nameGasPrice;
                OnPropertyChanged(nameof(nameGasPrice));
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Please check your internet again and connect again or keep using offline", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            } 
        }

        #endregion
        ~DashboardViewModel()
        {

        }
    }
}
