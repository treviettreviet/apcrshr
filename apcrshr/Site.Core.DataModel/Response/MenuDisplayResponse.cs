using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class MenuDisplayResponse : BaseResponse
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
    }
}
