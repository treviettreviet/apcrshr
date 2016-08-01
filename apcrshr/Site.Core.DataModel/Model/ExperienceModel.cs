using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class ExperienceModel
    {
        public string WorkingID { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public System.DateTime WorkingStart { get; set; }
        public Nullable<System.DateTime> WorkingEnd { get; set; }
        public bool WorkingNow { get; set; }
        public string Duties { get; set; }
        public string YouthScholarshipID { get; set; }
    }
}
