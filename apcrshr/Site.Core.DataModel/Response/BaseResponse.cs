using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class BaseResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
