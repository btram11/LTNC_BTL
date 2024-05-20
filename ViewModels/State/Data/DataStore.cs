using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Accounts;
using ViewModels.State.Authentication;

namespace ViewModels.State.Data
{
    public class DataStore : IDataStore
    {
        private object _currentObject;
        private ObservableCollection<string> _fuelPrice = new ObservableCollection<string>();
        public object CurrentObject 
        { 
            get => _currentObject; 
            set
            {
                _currentObject = value;
                StateChanged?.Invoke();
            }
        }

        public ObservableCollection<string> FuelPrice
        {
            get => _fuelPrice;
            set
            {
                _fuelPrice = value;
                StateChanged?.Invoke();
            }
        }


        public event Action StateChanged;
        public DataStore()
        {

        }

        public IVehicleDataFirebase GetVehicle()
        {
            if (CurrentObject is TrailerFirebase)
                return (TrailerFirebase)CurrentObject;
            else if (CurrentObject is VehicleFirebase) 
                return (VehicleFirebase)CurrentObject;
            return null;
        }

    }
}
