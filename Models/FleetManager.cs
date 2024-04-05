using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Vehicles;

namespace Models
{
    public enum VehicleType
    {
        Car,
        Truck,
        Bus
    }

    public enum LicenseType
    {
        B2, C, D, E, FC
    }
    public class FleetManager
    {
        private List<Vehicle> vehicles;
        //private List<Vehicle> cars;
        //private List<Vehicle> trucks;
        //private List<Vehicle> bus;
        private List<Driver> drivers;
        private List<Trailer> trailers;
        public FleetManager()
        {
            vehicles = new List<Vehicle>();
            drivers = new List<Driver>();
        }


        #region Method about Vehicle
        public void addVehicle(Vehicle vehicle)
        {
            foreach (Vehicle existingVehicle in vehicles)
            {
                if (existingVehicle._LicensePlate == vehicle._LicensePlate)
                {
                    throw new Exception("Existing Vehicle. Please check in Vehicle Tab");
                }
            }
            vehicles.Add(vehicle);
        }

        public void updateVehicle(Vehicle vehicle)
        {

        }

        public IEnumerable<Vehicle> GetCars()
        {
            return vehicles.Where(v => v is Car);
        }

        public IEnumerable<Vehicle> GetBussess()
        {
            return vehicles.Where(v => v is Bus);
        }

        public IEnumerable<Vehicle> GetTrucks()
        {
            return vehicles.Where(v => v is Truck);
        }

        public IEnumerable<Vehicle> GetVehiclesByType(VehicleType type)
        {
            string typeName = Enum.GetName(typeof(VehicleType), type);
            Type vehicleType = typeof(VehicleType).Assembly.GetTypes()
                .FirstOrDefault(t => t.Name == typeName);
            return vehicles.Where(v => v.GetType() == vehicleType);
        }
        #endregion


        public void addDriver(Driver driver)
        {
            foreach (Driver existingDriver in drivers)
            {
                if (existingDriver.license.LicenseNumber.Equals(driver.license.LicenseNumber))
                {
                    throw new Exception();
                }
            }
            drivers.Add(driver);
        }

        public void addTrip(Trip trip, TripType type)
        {
            // Choose vehicle and driver first then add these to trip
            // To do
            if (type == TripType.people && trip._Load <= 9)
            {
                trip.vehicle = GetCars()
                    .Where(v => v._seats >= trip._Load && v._seats - trip._Load < 10)
                    .OrderByDescending(v => v._fuelEfficiency)
                    .ThenBy(v => v._seats)
                    .FirstOrDefault();
            }
            else if (type == TripType.people && trip._Load > 9)
            {
                trip.vehicle = GetVehiclesByType(VehicleType.Bus)
                    .Where(v => v._seats >= trip._Load && v._seats - trip._Load < 10)
                    .OrderByDescending(v => v._fuelEfficiency)
                    .ThenBy(v => v._seats)
                    .FirstOrDefault();
            }
            else
            {
                IEnumerable<Vehicle> trucks = GetVehiclesByType(VehicleType.Truck);
            }
            trip.driver = drivers.FirstOrDefault();
        }

        public void addTrailer(Trailer trailer)
        {
            if (trailers == null)
            {
                trailers = new List<Trailer>();
            }
            trailers.Add(trailer);
        }



        public IEnumerable<Trailer> GetTrailers(int freight)
        {
            return trailers.Where(t => t._PayLoadCapacity >= freight).OrderBy(t => t._PayLoadCapacity).ThenBy(t => t._TrailerWeight);
        }
    }
}
