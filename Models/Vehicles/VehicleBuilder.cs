using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{

    public class VehicleBuilder
    {
        private Vehicle vehicle;

        public VehicleBuilder(string type)
        {
            //Thực hiện lại việc xử lý string và đưa ra phân loại
            type = type.ToLower();
            type = char.ToUpper(type[0]) + type.Substring(1);
            //string truckType = type.Contains("truck")
            switch ((VehicleType)Enum.Parse(typeof(VehicleType), type))
            {
                case VehicleType.Car:
                    vehicle = new Car();
                    break;
                case VehicleType.Truck:
                    vehicle = new Truck();
                    break;
                case VehicleType.Bus:
                    vehicle = new Bus();
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
        }

        public VehicleBuilder SetBrandModel(string brand, string model)
        {
            vehicle._Brand = brand;
            vehicle._Models = model;
            return this;
        }

        public VehicleBuilder SetYear(int year)
        {
            vehicle._Year = year;
            return this;
        }

        public VehicleBuilder SetLicensePlate(string number)
        {
            vehicle._LicensePlate = number;
            return this;
        }

        public VehicleBuilder SetEngine(int power, int size)
        {
            vehicle._EnginePower = power;
            vehicle._EngineSize = size;
            return this;
        }

        public VehicleBuilder SetFuel(FuelType fuel, int capacity)
        {
            vehicle._fuelType = fuel;
            vehicle._fuelCapacity = capacity;
            return this;
        }

        public VehicleBuilder SetType(object type)
        {
            vehicle.setType(type);
            return this;
        }

        public VehicleBuilder SetSeats(int seats)
        {
            vehicle.setSeats(seats);
            return this;
        }

        public VehicleBuilder AddTrip(Trip newTrip)
        {
            vehicle.addTrip(newTrip);
            return this;
        }

        public VehicleBuilder AddMaintain(Maintenance newMaintain)
        {
            vehicle.addMaintain(newMaintain);
            return this;
        }

        // Phương thức để thiết lập các thuộc tính riêng cho mỗi loại phương tiện
        //public VehicleBuilder SetTransmission(string transmission)
        //{
        //    if (vehicle is Car car)
        //    {
        //        car.Transmission = transmission;
        //    }
        //    return this;
        //}

        //public VehicleBuilder SetPayloadCapacity(int payloadCapacity)
        //{
        //    if (vehicle is Truck truck)
        //    {
        //        truck.PayloadCapacity = payloadCapacity;
        //    }
        //    return this;
        //}

        public Vehicle Build()
        {
            return vehicle;
        }
    }

}
