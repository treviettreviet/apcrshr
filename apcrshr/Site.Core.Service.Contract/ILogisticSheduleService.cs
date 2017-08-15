using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface ILogisticSheduleService
    {
        /// <summary>
        /// Find logistic by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<LogisticScheduleModel> FindByID(string id);

        /// <summary>
        /// Find by user ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindItemReponse<LogisticScheduleModel> FindByUserID(string userID);

        /// <summary>
        /// Delete logistic by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create logistic
        /// </summary>
        /// <param name="logistic"></param>
        /// <returns></returns>
        InsertResponse Create(LogisticScheduleModel logistic);

        /// <summary>
        /// Update logistic
        /// </summary>
        /// <param name="logistic"></param>
        /// <returns></returns>
        BaseResponse Update(LogisticScheduleModel logistic);

        /// <summary>
        /// Get all logistic
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<LogisticScheduleModel> GetAlls();
    }
}
