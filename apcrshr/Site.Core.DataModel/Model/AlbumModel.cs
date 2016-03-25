using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Site.Core.DataModel.Model
{
    public class AlbumModel
    {
        public string AlbumID { get; set; }
        public string Title { get; set; }
        public string ActionURL { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Description { get; set; }
        public IList<PhotoModel> Photos { get; set; }
    }
}
