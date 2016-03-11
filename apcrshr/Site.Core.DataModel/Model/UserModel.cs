using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class UserModel
    {
        public string UserID { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string Title { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string FullName { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string Sex { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Email_Invalid", ErrorMessage = null)]
        public string Email { get; set; }
        
        public string OtherEmail { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public System.DateTime DateOfBirth { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Required")]
        public string UserName { get; set; }
        
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Site.Core.DataModel.Resources.Resources), ErrorMessageResourceName = "Field_Password_Not_Match")]
        public string ConfirmPassword { get; set; }
        
        public System.DateTime CreatedDate { get; set; }
        
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        
        public bool Locked { get; set; }
    }
}
