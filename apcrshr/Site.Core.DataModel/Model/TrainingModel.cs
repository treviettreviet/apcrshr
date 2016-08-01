using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class TrainingModel
    {
        public string TrainingID { get; set; }
        public string NameOfCourse { get; set; }
        public string Country { get; set; }
        public System.DateTime TrainingStart { get; set; }
        public Nullable<System.DateTime> TrainingEnd { get; set; }
        public bool TrainingNow { get; set; }
        public string YouthScholarshipID { get; set; }
    }
}
