using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class MenuRepository : IMenuRepository
    {
        public object Insert(Menu item)
        {
            APCRSHREntities context = new APCRSHREntities();
            context.Menus.Add(item);
            context.SaveChanges();
            return item.MenuID;
        }

        public void Update(Menu item)
        {
            APCRSHREntities context = new APCRSHREntities();
            var menu = context.Menus.Where(a => a.MenuID.Equals(item.MenuID)).SingleOrDefault();
            if (menu != null)
            {
                menu.ActionURL = item.ActionURL;
                menu.Language = item.Language;
                menu.ParentID = item.ParentID;
                menu.Title = item.Title;

                context.SaveChanges();
            }
            else
            {
                throw new Exception(string.Format("Menu id {0} invalid", item.MenuID));
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var menu = context.Menus.Where(a => a.MenuID.Equals(_id)).SingleOrDefault();
                if (menu != null)
                {
                    context.Menus.Remove(menu);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Menu id {0} invalid", id));
                }
            }
        }

        public Menu FindByID(object id)
        {
            APCRSHREntities context = new APCRSHREntities();
            var _id = id.ToString();
            return context.Menus.Where(a => a.MenuID.Equals(_id)).SingleOrDefault();
        }

        public IList<Menu> FindAll()
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(m => m.ParentID == null).OrderBy(m => m.CreatedDate).ToList();
        }

        public IList<Menu> FindSubMenus(string parentID)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(m => m.ParentID.Equals(parentID)).OrderBy(m => m.CreatedDate).ToList();
        }


        public IList<Menu> FindAll(string language)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(m => m.Language.Equals(language)).OrderBy(m => m.CreatedDate).ToList();
        }


        public Menu FindByTitle(string title)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(a => a.Title.Equals(title)).SingleOrDefault();
        }
    }
}
