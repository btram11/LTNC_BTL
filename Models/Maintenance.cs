using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Maintenance
    {
        public int maintenanceID {  get; set; }
        public DateTime scheduledDate { get; set; }
        public DateTime comTime { get; set;}
    }
}
