using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FleetManager
    {
        private List<Vehicle> vehicles;
        private List<Driver> drivers;
        public FleetManager()
        {
            vehicles = new List<Vehicle>();
            drivers = new List<Driver>();
        }

        public void addVehicle(Vehicle vehicle)
        {
            foreach (Vehicle existingVehicle in vehicles)
            {
                if (existingVehicle.LicensePlate == vehicle.LicensePlate) 
                {
                    throw new Exception();
                }
            }
            vehicles.Add(vehicle);
        }

        public void updateVehicle(Vehicle vehicle)
        {

        }

        public void addDriver(Driver driver) 
        {
            foreach(Driver existingDriver in drivers)
            {
                if (existingDriver.license.LicenseNumber.Equals(driver.license.LicenseNumber))
                {
                    throw new Exception();
                }
            } 
            drivers.Add(driver);
        }

        public void addTrip(Trip trip)
        {
            // Choose vehicle and driver first then add these to trip
            // To do
            trip.vehicle = vehicles.FirstOrDefault();
            trip.driver = drivers.FirstOrDefault();
        }
    }
}
