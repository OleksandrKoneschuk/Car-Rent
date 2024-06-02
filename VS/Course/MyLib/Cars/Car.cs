using MyLib.Cars;
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
        public RentalCost RentalCosts { get; set; }
        public string CarBrandModel => $"{CarBrand} {CarModel}";
    }
}