using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Cars
{
    public class ElectricCar : Car
    {
        public int BatteryCapacity { get; set; }
        public int RangePerCharge { get; set; }
    }
}
