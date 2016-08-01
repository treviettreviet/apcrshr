using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class LeaderShipModel
    {
        public string LeaderShipID { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool LeaderNow { get; set; }
        public string Duties { get; set; }
        public string YouthScholarshipID { get; set; }
    }
}
