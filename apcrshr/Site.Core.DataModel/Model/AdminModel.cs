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
        public string AdminID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Email_Invalid", ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public System.DateTime BirthDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public bool Locked { get; set; }

        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Password_Not_Match")]
        public string ConfirmPassword { get; set; }
    }
}