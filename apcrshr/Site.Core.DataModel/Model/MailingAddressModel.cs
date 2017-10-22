using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class MailingAddressModel
    {
        public string MailingAddressID { get; set; }
        public string UserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ParticipantType { get; set; }
        public string ParticipateYouth { get; set; }
        public string OriginalNationality { get; set; }
        public string CurrentNationality { get; set; }
        public string PassportNumber { get; set; }
        public string TypeOfPassport { get; set; }
        public string Occupation { get; set; }
        public Nullable<System.DateTime> DateOfPassportIssue { get; set; }
        public Nullable<System.DateTime> DateOfPassportExpiry { get; set; }
        public string PassportPhoto1 { get; set; }
        public string PassportPhoto2 { get; set; }
        public string PassportPhoto3 { get; set; }
        public string DetailOfEmbassy { get; set; }
        public bool NeedVisaSupport { get; set; }
        public string ActivationCode { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
