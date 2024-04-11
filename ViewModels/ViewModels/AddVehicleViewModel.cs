using Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    
    public class AddVehicleViewModel : ViewModelBase
    {
        private ObservableCollection<Vehicle> listVehicle;
        public ObservableCollection<Vehicle> ListVehicle
        {
            get { return ListVehicle; }
            set { ListVehicle = value; }
        }

        public AddVehicleViewModel()
        {
            listVehicle = new ObservableCollection<Vehicle>();

            BackToVehicleListView = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                Window window = Window.GetWindow(p);
                var viewModel = (MainViewModel)window.DataContext;
                viewModel.currentViewModel = new VehicleListViewModel();
            });

        }

        public ICommand AddVehicleCommand { get; set; }
        public ICommand DecodeVinCommand { get; set; }

        public ICommand BackToVehicleListView { get; set; }
    }
}
