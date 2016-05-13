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
                menu.Type = item.Type;
                if (!string.IsNullOrEmpty(item.URL))
                {
                    menu.URL = item.URL;
                }
                menu.DisplayOrder = item.DisplayOrder;

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
            if (id == null) return null;
            APCRSHREntities context = new APCRSHREntities();
            var _id = id.ToString();
            return context.Menus.Where(a => a.MenuID.Equals(_id)).SingleOrDefault();
        }

        public IList<Menu> FindAll()
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(m => m.ParentID == null).OrderBy(m => m.DisplayOrder).ToList();
        }

        public IList<Menu> FindSubMenus(string parentID)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(m => m.ParentID.Equals(parentID)).OrderBy(m => m.DisplayOrder).ToList();
        }


        public IList<Menu> FindAll(string language)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(m => m.Language.Equals(language) && m.ParentID == null).OrderBy(m => m.DisplayOrder).ToList();
        }


        public Menu FindByTitle(string title)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(a => a.Title.Equals(title)).SingleOrDefault();
        }

        public Menu FindByActionURL(string actionURL)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
        }


        public Menu FindByTitleAndParent(string title, string parentID)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(a => a.Title.Equals(title) && a.ParentID.Equals(parentID)).SingleOrDefault();
        }


        public Menu FindByTitle(string title, string language)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(a => a.Title.Equals(title) && a.Language.Equals(language)).SingleOrDefault();
        }

        public Menu FindParentByTitle(string title, string language)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Menus.Where(a => a.Title.Equals(title) && a.ParentID == null && a.Language.Equals(language)).SingleOrDefault();
        }
    }
}
