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
    
    public partial class DownloadCode
    {
        public string DownloadID { get; set; }
        public string Code { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ValidDate { get; set; }
        public string UserID { get; set; }
        public string PaymentID { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
