using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vehicles
{
    public class Car: Vehicle
    {
        public object _Type { get; set; }
        public Car()
        {

        }

        public override void setType(object type)
        {
            
        }

        public override void setSeats(int seats = 2)
        {
            this._seats = seats;
        }
    }
}
