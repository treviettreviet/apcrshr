using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface IYouthScholarshipService
    {
        /// <summary>
        /// Find Youth Scholarship by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<YouthScholarshipModel> FindByID(string id);

        /// <summary>
        /// Delete Youth Scholarship by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create Youth Scholarship
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        InsertResponse Create(YouthScholarshipModel scholarship);

        /// <summary>
        /// Update Youth Scholarship
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        BaseResponse Update(YouthScholarshipModel scholarship);

        /// <summary>
        /// Get all Youth Scholarship
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<YouthScholarshipModel> GetAlls();

        /// <summary>
        /// Get youth scholarship by user ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindItemReponse<YouthScholarshipModel> FindByUserID(string userID);
    }
}
