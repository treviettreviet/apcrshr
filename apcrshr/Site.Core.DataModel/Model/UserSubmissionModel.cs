using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class UserSubmissionModel
    {
        public string SubmitID { get; set; }
        public string UserID { get; set; }
        public string SubmissionNumber { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
