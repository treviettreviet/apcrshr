using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class TransactionHistoryModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Action { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Log { get; set; }
    }
}
