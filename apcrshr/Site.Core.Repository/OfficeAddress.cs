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
    
    public partial class OfficeAddress
    {
        public string OfficeAddressID { get; set; }
        public string Institute { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string UserID { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual User User { get; set; }
    }
}
