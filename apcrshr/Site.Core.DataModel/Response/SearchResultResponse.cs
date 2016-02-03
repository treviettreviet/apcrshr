using Site.Core.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class SearchResultResponse : BaseResponse
    {
        public IList<SearchModel> Result { get; set; }
        public int Count { get; set; }
        public string KeySearch { get; set; }
        public int PageIndex { get; set; }
    }
}
