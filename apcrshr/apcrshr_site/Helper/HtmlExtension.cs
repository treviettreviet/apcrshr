using Site.Core.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace apcrshr_site.Helper
{
    public static class HtmlExtension
    {
        public static IHtmlString TreeMenu(this HtmlHelper htmlHelper, IList<MenuModel> menus)
        {
            StringBuilder builder = new StringBuilder();

            //Start element
            builder.Append("<ul id='browser' class='filetree'>");

            //Build menu
            BuildMenuElement(menus, builder);

            //End element
            builder.Append("</ul>");

            return htmlHelper.Raw(builder.ToString());
        }

        private static void BuildMenuElement(IList<MenuModel> menus, StringBuilder builder)
        {
            foreach (MenuModel m in menus)
            {
                builder.Append("<li>");

                if (m.SubMenus != null && m.SubMenus.Count > 0)
                {
                    IList<MenuModel> list = m.SubMenus.OrderBy(s => s.CreatedDate).ToList();
                    builder.Append("<span id='" + m.MenuID + "' class='folder'>" + m.Title + "</span>");
                    builder.Append("<ul>");
                    foreach (MenuModel sub in list)
                    {
                        builder.Append("<li><span id='" + sub.MenuID + "' class='file'>" + sub.Title + "</span>" +
                        "<a href='/Administrator/Admin/UpdateCategory?id=" + sub.MenuID + "' class='btn' rel='tooltip' title='Sửa'><i class='fa fa-edit'></i></a>&nbsp;" +
                        "<a href='#' onclick='deleteMenu(\"" + sub.MenuID + "\")' class='btn' rel='tooltip' title='Xóa'><i class='fa fa-times'></i></a></li>");
                    }
                    builder.Append("</ul>");
                }
                else
                {
                    builder.Append("<span id='" + m.MenuID + "' class='file'>" + m.Title + "</span>" +
                    "<a href='/Administrator/Admin/UpdateCategory?id=" + m.MenuID + "' class='btn' rel='tooltip' title='Sửa'><i class='fa fa-edit'></i></a>&nbsp;" +
                    "<a href='#' onclick='deleteMenu(\"" + m.MenuID + "\")' class='btn' rel='tooltip' title='Xóa'><i class='fa fa-times'></i></a>");
                }

                builder.Append("</li>");
            }
        }

        /// <summary>
        /// URL: {controller}/{action}/{page}
        /// ex. ProgramController/Index
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="ActionURL">Page index (controller/action/actionURL - page index)</param>
        /// <returns></returns>
        public static IHtmlString PagingUtil(this HtmlHelper htmlHelper, string action, string controller, 
            int count, int pageSize, KeyValuePair<string, object> index)
        {
            StringBuilder builder = new StringBuilder();

            int totalPage = (count / pageSize) + 1;
            int pageIndex = 1;
            if (index.Value == null)
            {
                pageIndex = 1;
            }
            else
            {
                int.TryParse(index.Value.ToString(), out pageIndex);
            }

            if (totalPage > 1)
            {
                int currentPage = pageIndex;

                builder.Append("<span class='all-pages'>" + apcrshr_site.Resources.Resource.text_page + " " + currentPage + " " + apcrshr_site.Resources.Resource.text_of + " " + totalPage + "</span>");

                if (currentPage > 1)
                {
                    builder.Append("<a class='next-page' href='/" + controller + "/" + action + "/" + (currentPage - 1) + "'>"+Resources.Resource.button_previous+"</a>");
                }

                if (currentPage == 1)
                {
                    builder.Append("<span class='current page-num'>" + 1 + "</span>");
                }
                else
                {
                    builder.Append("<a class='next-page' href='/" + controller + "/" + action + "/" + 1 + "'>1</a>");
                }

                if (currentPage > 2)
                {
                    builder.Append("<span class='page-num'>...</span>");
                    if (currentPage == totalPage && totalPage > 3)
                    {
                        builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + (currentPage - 2) + "'>" + (currentPage - 2) + "</a>");
                    }
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + (currentPage - 1) + "'>" + (currentPage - 1) + "</a>");
                }

                if (currentPage != 1 && currentPage != totalPage)
                {
                    builder.Append("<span class='current page-num'>" + currentPage + "</span>");
                }

                if (currentPage < totalPage - 1)
                {
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + (currentPage + 1) + "'>" + (currentPage + 1) + "</a>");
                    if (currentPage == 1 && totalPage > 3)
                    {
                        builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + (currentPage + 2) + "'>" + (currentPage + 2) + "</a>");
                    }
                    builder.Append("<span class='page-num'>...</span>");
                }

                if (currentPage == totalPage)
                {
                    builder.Append("<span class='current page-num'>" + totalPage + "</span>");
                }
                else
                {
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + totalPage + "'>" + totalPage + "</a>");
                }

                if (currentPage < totalPage)
                {
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + (currentPage + 1) + "'>" + Resources.Resource.button_next + "</a>");
                }
            }

            return htmlHelper.Raw(builder.ToString());
        }

        /// <summary>
        /// URL: {controller}/{action}/{ActionURL}/{page}
        /// ex. ProgramController/ProgramDetail
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="actionURL"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IHtmlString PagingUtil(this HtmlHelper htmlHelper, string action, string controller, string actionURL, 
            int count, int pageSize, KeyValuePair<string, object> index)
        {
            StringBuilder builder = new StringBuilder();

            int totalPage = (count / pageSize) + 1;
            int pageIndex = 1;
            if (index.Value == null)
            {
                pageIndex = 1;
            }
            else
            {
                int.TryParse(index.Value.ToString(), out pageIndex);
            }

            if (totalPage > 1)
            {
                int currentPage = pageIndex;

                builder.Append("<span class='all-pages'>" + apcrshr_site.Resources.Resource.text_page + " " + currentPage + " " + apcrshr_site.Resources.Resource.text_of + " " + totalPage + "</span>");

                if (currentPage > 1)
                {
                    builder.Append("<a class='next-page' href='/" + controller + "/" + action + "/" + actionURL + "/" + (currentPage - 1) + "'>" + Resources.Resource.button_previous + "</a>");
                }

                if (currentPage == 1)
                {
                    builder.Append("<span class='current page-num'>" + 1 + "</span>");
                }
                else
                {
                    builder.Append("<a class='next-page' href='/" + controller + "/" + action + "/" + actionURL + "/" + 1 + "'>1</a>");
                }

                if (currentPage > 2)
                {
                    builder.Append("<span class='page-num'>...</span>");
                    if (currentPage == totalPage && totalPage > 3)
                    {
                        builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + actionURL + "/" + (currentPage - 2) + "'>" + (currentPage - 2) + "</a>");
                    }
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + actionURL + "/" + (currentPage - 1) + "'>" + (currentPage - 1) + "</a>");
                }

                if (currentPage != 1 && currentPage != totalPage)
                {
                    builder.Append("<span class='current page-num'>" + currentPage + "</span>");
                }

                if (currentPage < totalPage - 1)
                {
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + actionURL + "/" + (currentPage + 1) + "'>" + (currentPage + 1) + "</a>");
                    if (currentPage == 1 && totalPage > 3)
                    {
                        builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + actionURL + "/" + (currentPage + 2) + "'>" + (currentPage + 2) + "</a>");
                    }
                    builder.Append("<span class='page-num'>...</span>");
                }

                if (currentPage == totalPage)
                {
                    builder.Append("<span class='current page-num'>" + totalPage + "</span>");
                }
                else
                {
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + actionURL + "/" + totalPage + "'>" + totalPage + "</a>");
                }

                if (currentPage < totalPage)
                {
                    builder.Append("<a class='page-num' href='/" + controller + "/" + action + "/" + actionURL + "/" + (currentPage + 1) + "'>" + Resources.Resource.button_next + "</a>");
                }
            }

            return htmlHelper.Raw(builder.ToString());
        }
    }
}