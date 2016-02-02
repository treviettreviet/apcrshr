using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class SearchModel
    {
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ActionURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageURL { get; set; }
    }
}
