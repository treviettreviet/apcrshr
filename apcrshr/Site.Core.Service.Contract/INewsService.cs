using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface INewsService
    {
        /// <summary>
        /// Find news by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<NewsModel> FindNewsByID(string id);

        /// <summary>
        /// Get news detail
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<NewsModel> FindNewsByActionURL(string actionURL);

        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteNews(string id);

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        BaseResponse UpdateNews(NewsModel news);

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<NewsModel> GetNews();

        /// <summary>
        /// Get news with paging
        /// </summary>
        /// <param name="pageSize">int</param>
        /// <param name="pageIndex">int</param>
        /// <returns></returns>
        FindAllItemReponse<NewsModel> GetNews(int pageSize, int pageIndex);

        /// <summary>
        /// Get news with pagging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        FindAllItemReponse<NewsModel> GetNews(int pageSize, int pageIndex, string language);

        /// <summary>
        /// Get all related news by date
        /// </summary>
        /// <param name="category"></param>
        /// <param name="date"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        FindAllItemReponse<NewsModel> GetRelatedNews(DateTime date, int pageSize, int pageIndex, string language);

        /// <summary>
        /// Create news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        InsertResponse CreateNews(NewsModel news);

        /// <summary>
        /// Get news by ID
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        FindItemReponse<NewsModel> GetNewsByID(string newsID);

        /// <summary>
        /// Get top News
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        FindAllItemReponse<NewsModel> GetTopNews(int top, string language);
    }
}
