using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IHomeService
    {
        /// <summary>
        /// Get top News
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        FindAllItemReponse<NewsModel> GetTopNews(int top, string language);

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        SearchResultResponse Search(string keyword, int pageSize, int pageIndex);
    }
}
