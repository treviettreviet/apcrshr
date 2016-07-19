using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class SessionModel
    {
        public string SessionID { get; set; }
        public string Options { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int Step { get; set; }
        public bool Completed { get; set; }
    }
}
