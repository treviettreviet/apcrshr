using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Model
{
    public class MenuModel
    {
        public string MenuID { get; set; }
        public string Title { get; set; }
        public string ActionURL { get; set; }
        public string Language { get; set; }
        public string ParentID { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<MenuModel> SubMenus { get; set; }
        public MenuModel Parent { get; set; }
        public bool Type { get; set; }
        public string URL { get; set; }
        public int DisplayOrder { get; set; }
    }
}
