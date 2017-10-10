using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class UserExportModel
    {
        //Users
        public string UserID { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string OtherEmail { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool Locked { get; set; }
        public string MealPreference { get; set; }
        public string DisabilitySpecialTreatment { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string WorkAddress { get; set; }
        public string Organization { get; set; }
        public string ParticipantType { get; set; }

        //Mailing
        public string ParticipateYouth { get; set; }
        public string OriginalNationality { get; set; }
        public string CurrentNationality { get; set; }
        public string PassportNumber { get; set; }
        public string TypeOfPassport { get; set; }
        public string Occupation { get; set; }
        public string DateOfPassportIssue { get; set; }
        public string DateOfPassportExpiry { get; set; }
        public string DetailOfEmbassy { get; set; }
        public bool NeedVisaSupport { get; set; }
        public string ActivationCode { get; set; }
        public string RegistrationNumber { get; set; }

        //Main scholarship
        public string SubmissionNumber { get; set; }
        public string Responsibility { get; set; }
        public string ReasonScholarship { get; set; }
        public bool AtLeastOneAbstract { get; set; }
        public string Position { get; set; }
        public int DurationOfWork { get; set; }
        public string SubmissionTitles { get; set; }
        public string ScholarshipPackage { get; set; }

        //Payment
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        public string PaidDate { get; set; }
        public string Status { get; set; }

        //Logistic
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalFlightNumber { get; set; }
        public string ArrivalGate { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string DepartureGate { get; set; }
        public string WhenNeedPick { get; set; }
        public string SpecialRequirement { get; set; }
        public string HotelName { get; set; }
        public Nullable<DateTime> CheckinDate { get; set; }
        public Nullable<DateTime> CheckoutDate { get; set; }
    }
}
