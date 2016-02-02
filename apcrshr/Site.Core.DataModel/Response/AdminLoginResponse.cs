using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class AdminLoginResponse : BaseResponse
    {
        public string SessionId { get; set; }

        public string AdminId { get; set; }

        public string AdminName { get; set; }
    }
}
