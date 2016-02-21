using Site.Core.DataModel.Response;
using System;
using Site.Core.DataModel.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IImportantDeadlineService
    {
        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<ImportantDeadlineModel> FindImportantByID(string id);
        /// <summary>
        /// Find by actionUrl
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        FindItemReponse<ImportantDeadlineModel> FindImportantByActionURL(string url);
        /// <summary>
        /// Update important deadline
        /// </summary>
        /// <param name="importantDeadline"></param>
        /// <returns></returns>
        BaseResponse UpdateImportantDealine(ImportantDeadlineModel importantDeadline);
        /// <summary>
        /// Get all impotant deadline
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<ImportantDeadlineModel> GetImportantDeadlines();
        /// <summary>
        /// Get all important pagging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        FindAllItemReponse<ImportantDeadlineModel> GetImportantDeadlines(int pageSize, int pageIndex);
        /// <summary>
        /// Create Important deadline
        /// </summary>
        /// <param name="importantDeadline"></param>
        /// <returns></returns>
        InsertResponse CreateImportantDeadline(ImportantDeadlineModel importantDeadline);
        /// <summary>
        /// Delete a important deadline
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteImportantDeadline(string id);
        /// <summary>
        /// Find important deadline by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        FindItemReponse<ImportantDeadlineModel> GetImportantDeadlineByTitle(string title);
        /// <summary>
        /// Get all important deadline top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        FindAllItemReponse<ImportantDeadlineModel> GetTopImportantDeadlines(int top);

    }
}
