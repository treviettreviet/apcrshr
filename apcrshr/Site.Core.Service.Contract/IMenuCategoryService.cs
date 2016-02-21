using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IMenuCategoryService
    {
        /// <summary>
        /// Find all menus
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<MenuModel> FindAllMenus();

        /// <summary>
        /// Find all menus with language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        FindAllItemReponse<MenuModel> FindAllMenus(string language);

        /// <summary>
        /// Create new menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        InsertResponse CreateMenu(MenuModel menu);

        /// <summary>
        /// Update menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        BaseResponse UpdateMenu(MenuModel menu);

        /// <summary>
        /// Delete menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteMenu(string id);

        /// <summary>
        /// Find by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<MenuModel>  FindByID(string id);

        /// <summary>
        /// Find menu by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        FindItemReponse<MenuModel> FindByTitle(string title);

        /// <summary>
        /// Find by action url
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<MenuModel> FindByActionURL(string actionURL);
    }
}
