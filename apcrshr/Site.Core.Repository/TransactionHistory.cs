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
    
    public partial class TransactionHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Log { get; set; }
    }
}
