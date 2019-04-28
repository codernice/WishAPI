using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Wish.Model
{
    public class ModifyTrackingDto
    {
        public string OrderID { get; set; }
        public string TrackingName { get; set; }
        public string TrackingNumber { get; set; }
        public string CountryCode { get; set; }
        public string ShipNote { get; set; }
    }
}
