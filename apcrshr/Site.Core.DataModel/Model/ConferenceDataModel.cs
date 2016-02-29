using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Site.Core.DataModel.Model
{
    public class ConferenceDataModel
    {
        public string ConferenceDataID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
        public string ActionURL { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Language { get; set; }
        public string MenuID { get; set; }
        public string DocURL { get; set; }
        public string ImageURL { get; set; }
    }
}
