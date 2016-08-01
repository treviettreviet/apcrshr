using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class EducationModel
    {
        public string EducationID { get; set; }
        public string Degree { get; set; }
        public string Country { get; set; }
        public System.DateTime EducationStart { get; set; }
        public Nullable<System.DateTime> EducationEnd { get; set; }
        public bool EducationNow { get; set; }
        public string MainCourseStudy { get; set; }
        public string TypeOfTraining { get; set; }
        public string YouthScholarshipID { get; set; }
    }
}
