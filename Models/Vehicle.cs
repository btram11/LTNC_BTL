using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum FuelType
    {
        Gasoline,
        Diesle,
        Electric
    }
    public abstract class Vehicle
    {
        public int VehicleStatus { get; set; }
        public string Brand { get; set; }
        public string Models { get; set; }
        public string LicensePlate { get; set; }
        protected int EnginePower { get; set; }
        public FuelType fuelType { get; set; }
        protected int fuelCapacity { get; set; }
        protected int nb_of_wheeles;
        public DateTime latestMaintenanceDay { get; set; }
        public DateTime nextScheduledMaintenance { get; set;}
        public List<Trip> tripList { get; set; }
        protected Vehicle(int nb_of_wheeles) 
        {
            this.nb_of_wheeles = nb_of_wheeles;
            this.tripList = new List<Trip>();
        }

        public void addTrip(Trip trip)
        {
            tripList.Add(trip);
        }
    }
}
