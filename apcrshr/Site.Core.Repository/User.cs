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
    
    public partial class User
    {
        public User()
        {
            this.MailingAddresses = new HashSet<MailingAddress>();
        }
    
        public string UserID { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string OtherEmail { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool Locked { get; set; }
        public string MealPreference { get; set; }
        public string DisabilitySpecialTreatment { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string WorkAddress { get; set; }
        public string Organization { get; set; }
        public int RegistrationStatus { get; set; }
        public string EmailDuplicationCode { get; set; }
    
        public virtual ICollection<MailingAddress> MailingAddresses { get; set; }
    }
}
