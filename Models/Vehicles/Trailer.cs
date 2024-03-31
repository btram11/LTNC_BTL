using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{
    public class Trailer
    {
        public string _LicenseNumber {  get; set; }
        public int _PayLoadCapacity { get; set; }
        public int _TrailerWeight { get; set; }
        public bool _HasCargo = false;
        public Vehicle _attachedVehicle = null;
        public Trailer() { }
    }
}
