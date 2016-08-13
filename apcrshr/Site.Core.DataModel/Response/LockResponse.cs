using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class LockResponse : BaseResponse
    {
        public bool Locked { get; set; }
    }
}
