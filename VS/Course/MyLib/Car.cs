using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class Car
    {
        public int ID_Avto { get; set; }
        public string CarNumber { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public int CarYear { get; set; }
        public decimal AverageFuelConsumption { get; set; }
        public byte[] PhotoData { get; set; }
        public decimal Cost1_3DayRental { get; set; }
        public decimal Cost4_9DayRental { get; set; }
        public decimal Cost10_25DayRental { get; set; }
        public decimal Cost26DayRental { get; set; }
        public string CarBrandModel => $"{CarBrand} {CarModel}";
    }

    public class ElectricCar : Car
    {
        public int BatteryCapacity { get; set; } 
        public int RangePerCharge { get; set; } 
    }

    public class GasCar : Car
    {
        public decimal TankCapacity { get; set; } 
        public decimal FuelEfficiency { get; set; } 
    }
}