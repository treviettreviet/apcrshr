using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class AdminModel
    {
        public AdminModel()
        {
            this.Roles = new HashSet<RoleModel>();
        }
    
        public string AdminID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public bool Locked { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Type { get; set; }
        public virtual ICollection<RoleModel> Roles { get; set; }
    }
}