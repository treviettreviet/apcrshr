using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IMenuRepository : IRepository<Menu>
    {
        IList<Menu> FindSubMenus(string parentID);
        IList<Menu> FindAll(string language);
        Menu FindByTitle(string title);
    }
}
