//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Site.Core.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogisticSchedule
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
        public Nullable<System.DateTime> CheckinDate { get; set; }
        public Nullable<System.DateTime> CheckoutDate { get; set; }
        public string ConferenceRoles { get; set; }
        public string AirportService { get; set; }
        public string VisaProcess { get; set; }
    
        public virtual User User { get; set; }
    }
}
