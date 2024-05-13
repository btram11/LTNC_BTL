using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.State.Data
{
    public interface IDataStore
    {
        object CurrentObject { get; set; }
        ObservableCollection<string> FuelPrice {  get; set; }
        event Action StateChanged;

        public IVehicleDataFirebase GetVehicle();
    }
}
