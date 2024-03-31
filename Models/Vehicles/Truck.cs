using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{
    public enum DriveType
    {
        _4x2, _4x4, _6x2, _6x4, _8x2, _8x4
    }
    public class Truck: Vehicle
    {
        //public DriveType DriveType { get; }
        //public Truck(int vehicleStatus, string brand, string models, string licensePlate, int enginePower, FuelType fuelType, int fuelCapacity, int nb_of_wheeles) : base(nb_of_wheeles)
        //{
        //    _VehicleStatus = vehicleStatus;
        //    _Brand = brand;
        //    _Models = models;
        //    this._LicensePlate = licensePlate;
        //    this._EnginePower = enginePower;
        //    this._fuelType = fuelType;
        //    this._fuelCapacity = fuelCapacity;
        //}

        public Truck() { }
        public Trailer _trailer { get; set; }
        public DriveType _driveType { get; set; }
        public override void setType(object type)
        {
            if (type is DriveType driveType)
            {
                _driveType = driveType;
                this._nb_of_wheeles = int.Parse(_driveType.ToString().Substring(1, 1));
            }
        }

        public override void setSeats(int seats)
        {
            this._seats = seats;
        }
    }
}
