using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IUserSubmissionService
    {
        /// <summary>
        /// Find submission by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<UserSubmissionModel> FindByID(string id);

        /// <summary>
        /// Delete submission by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create submission
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        InsertResponse Create(UserSubmissionModel submission);

        /// <summary>
        /// Update submission
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        BaseResponse Update(UserSubmissionModel submission);

        /// <summary>
        /// Get all submission
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<UserSubmissionModel> GetAlls();

        /// <summary>
        /// Find all submission by user ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindAllItemReponse<UserSubmissionModel> FindByUserID(string userID);

        /// <summary>
        /// Find by submission number
        /// </summary>
        /// <param name="submissionNumber"></param>
        /// <returns></returns>
        FindItemReponse<UserSubmissionModel> FindBySubmissionNumber(string submissionNumber);
    }
}
