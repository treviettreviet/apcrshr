using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apcrshr_site.Models
{
    public class RegistrationModel
    {
        public string UserID { get; set; }
        public int CurrentStep { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string MealPreference { get; set; }
        public string DisabilityOrTreatment { get; set; }
        public string Email { get; set; }
        public string OtherEmail { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string WorkAddress { get; set; }
        public string Organization { get; set; }
        public string ParticipantType { get; set; }
        public bool YouthConference { get; set; }
        public bool NeedVisaSupport { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string OriginalNationality { get; set; }
        public string CurrentNationality { get; set; }
        public string PassportNumber { get; set; }
        public string TypeOfPassport { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfPassportIssue { get; set; }
        public DateTime DateOfPassportExpiry { get; set; }
        public string DetailOfEmbassy { get; set; }
        public int RegistrationStatus { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}