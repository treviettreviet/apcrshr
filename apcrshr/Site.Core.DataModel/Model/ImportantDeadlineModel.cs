using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Site.Core.DataModel.Model
{
    public class ImportantDeadlineModel
    {
        public string DeadlineID { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
        public string ActionURL { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime Deadline { get; set; }
        public int Type { get; set; }
    }
}
