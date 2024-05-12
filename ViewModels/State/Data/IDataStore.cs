using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.State.Data
{
    public interface IDataStore
    {
        object CurrentObject { get; set; }
        IVehicleDataFirebase Vehicle { get; }
        event Action StateChanged;

        public IVehicleDataFirebase GetVehicle();
    }
}
