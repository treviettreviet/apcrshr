using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IMainScholarshipService
    {
        /// <summary>
        /// Find Main Scholarship by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<MainScholarshipModel> FindByID(string id);

        /// <summary>
        /// Delete Main Scholarship by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create Main Scholarship
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        InsertResponse Create(MainScholarshipModel scholarship);

        /// <summary>
        /// Update Main Scholarship
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        BaseResponse Update(MainScholarshipModel scholarship);

        /// <summary>
        /// Get all Main Scholarship
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<MainScholarshipModel> GetAlls();

        /// <summary>
        /// Find main scholarship by user ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindAllItemReponse<MainScholarshipModel> FindByUserID(string userID);

        /// <summary>
        /// Find main scholarship by userID and submission number
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="submissionNumber"></param>
        /// <returns></returns>
        FindItemReponse<MainScholarshipModel> FindByUserIDAndSubmission(string userID, string submissionNumber);
    }
}
