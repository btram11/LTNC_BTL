using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum DriveType
    {
        _4x2, _4x4, _6x2, _6x4, _8x2, _8x4
    }
    public class Truck: Vehicle
    {
        public DriveType DriveType { get; }
        public Truck(int vehicleStatus, string brand, string models, string licensePlate, int enginePower, FuelType fuelType, int fuelCapacity, int nb_of_wheeles) : base(nb_of_wheeles)
        {
            VehicleStatus = vehicleStatus;
            Brand = brand;
            Models = models;
            this.LicensePlate = licensePlate;
            this.EnginePower = enginePower;
            this.fuelType = fuelType;
            this.fuelCapacity = fuelCapacity;

        }
    }
}
