using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IArticleService
    {
        /// <summary>
        /// Find by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<ArticleModel> FindArticleByID(string id);

        /// <summary>
        /// Find by action URL
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<ArticleModel> FindArticleByActionURL(string actionURL);

        /// <summary>
        /// Find by menu ID
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        FindAllItemReponse<ArticleModel> FindArticleByMenuID(string menuID);

        /// <summary>
        /// Delete article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteArticle(string id);

        /// <summary>
        /// Update article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        BaseResponse UpdateArticle(ArticleModel article);

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<ArticleModel> GetArticles();

        /// <summary>
        /// Get all article pagging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        FindAllItemReponse<ArticleModel> GetArticles(int pageSize, int pageIndex);

        /// <summary>
        /// Get all article pagging and language
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        FindAllItemReponse<ArticleModel> GetArticles(int pageSize, int pageIndex, string language);

        /// <summary>
        /// Find all articles by menu ID
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        FindAllItemReponse<ArticleModel> GetArticles(int pageSize, int pageIndex, string language, string menuID);

        /// <summary>
        /// Create new article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        InsertResponse CreateArticle(ArticleModel article);
    }
}
