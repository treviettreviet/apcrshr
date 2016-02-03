using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class UserLoginResponse : BaseResponse
    {
        public string SessionId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
