using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{
    public enum BusType
    {
        Coach,
        Sprinter,
        Bus
    }
    public class Bus: Vehicle
    {
        //public Bus (int vehicleStatus, string brand, string models, string licensePlate, int enginePower, FuelType fuelType, int fuelCapacity, int nb_of_wheeles)
        //{

        //}
        public int _height, _width, _length;
        public BusType _Type {  get; set; }

        public Bus() { }
        public override void setType(object type)
        {
            if (type is BusType busType)
            {
                _Type = busType;
            }
        }

        public override void setSeats(int seats = 2)
        {
            this._seats = seats;
        }
    }
}
