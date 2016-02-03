using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class SubscriberModel
    {
        public string SubscriberID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
