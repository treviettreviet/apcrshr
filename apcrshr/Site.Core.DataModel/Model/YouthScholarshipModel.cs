using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class YouthScholarshipModel
    {
        public string YouthScholarshipID { get; set; }
        public string RegistrationNumber { get; set; }
        public string MotivationIssue { get; set; }
        public string MotivationResolving { get; set; }
        public string MotivationReason { get; set; }
        public string PlanMaking { get; set; }
        public string UploadFile { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UserID { get; set; }
        public string ExperienceTitles { get; set; }
        public string EducationTitles { get; set; }
        public string AdditionalTrainingTitles { get; set; }
        public string LeaderShipTitles { get; set; }
        public string PublicationTitles { get; set; }
    }
}
