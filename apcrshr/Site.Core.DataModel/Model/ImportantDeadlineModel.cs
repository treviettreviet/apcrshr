using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class ImportantDeadlineModel
    {
        public string DeadlineID { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Contents { get; set; }
        public string ActionURL { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Deadline { get; set; }
    }
}
