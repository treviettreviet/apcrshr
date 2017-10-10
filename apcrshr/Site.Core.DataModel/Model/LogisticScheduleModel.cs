using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class LogisticScheduleModel
    {
        public string LogisticID { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public string ArrivalFlightNumber { get; set; }
        public string ArrivalGate { get; set; }
        public Nullable<System.DateTime> DepartureDate { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string DepartureGate { get; set; }
        public string WhenNeedPick { get; set; }
        public string SpecialRequirement { get; set; }
        public string UserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string HotelName { get; set; }
        public Nullable<DateTime> CheckinDate { get; set; }
        public Nullable<DateTime> CheckoutDate { get; set; }
    }
}
