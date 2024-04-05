using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{
    public enum FuelType
    {
        Gasoline,
        Diesle,
        Electric,
    }

    public abstract class Vehicle
    {
        #region attributes
        public int _VehicleStatus { get; set; }
        public string _Brand { get; set; }
        public string _Models { get; set; }
        public int _Year { get; set; }
        public string _LicensePlate { get; set; }
        public int _EnginePower { get; set; }
        public int _EngineSize { get; set; }
        public FuelType _fuelType { get; set; }
        public int _fuelCapacity { get; set; }
        public double _fuelEfficiency { get; set; }
        public int _seats { get; set; }
        public int GCWR { get; set; } //Gross Combined Weight Rating
        public int GVWR { get; set; } //Gross Vehicle Weight Rating
        public int CurbWeight { get; set; }

        public virtual LicenseType LicenseAtLeast => LicenseType.B2;
        protected int _nb_of_wheeles;

        private List<Maintenance> _maintainList { get; set; }
        private List<Trip> _tripList { get; set; }
        public string CurrentLocation { get; set; }

        #endregion

        public void addTrip(Trip trip)
        {
            if (_tripList == null)
            {
                _tripList = new List<Trip>();
            }
            _tripList.Add(trip);
        }

        public void addMaintain(Maintenance maintain)
        {
            if (_maintainList == null)
            {
                _maintainList = new List<Maintenance>();
            }
            _maintainList.Add(maintain);
        }

        public abstract void setType(object type);

        public abstract void setSeats(int seats = 2);

    }
}
