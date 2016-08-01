using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class PublicationModel
    {
        public string PublicationID { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public int Year { get; set; }
        public string URL { get; set; }
        public string YouthScholarshipID { get; set; }
    }
}
