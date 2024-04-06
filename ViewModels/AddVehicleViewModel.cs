using Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        public ICommand AddVehicleCommand { get; set; }
        public ICommand DecodeVin { get; set; }
    }
}
