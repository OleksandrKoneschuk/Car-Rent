using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public class BookingHistoryItem
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ActualEndDate { get; set; }
    }
}
