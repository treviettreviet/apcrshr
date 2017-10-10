using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class UserModel
    {
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
        public string ParticipantType { get; set; }
        public string MainScholarship { get; set; }
        public string YouthScholarship { get; set; }
        public LogisticScheduleModel Logistic { get; set; }
    }
}
