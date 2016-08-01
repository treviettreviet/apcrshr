using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class MainScholarshipModel
    {
        public string ScholarshipID { get; set; }
        public string RegistrationNumber { get; set; }
        public string SubmissionNumber { get; set; }
        public string Responsibility { get; set; }
        public string ReasonScholarship { get; set; }
        public bool HasSubmitted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public System.DateTime WorkingStart { get; set; }
        public Nullable<System.DateTime> WorkingEnd { get; set; }
        public bool WorkingNow { get; set; }
    }
}
