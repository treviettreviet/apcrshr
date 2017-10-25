using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface ITransactionHistoryService
    {
        /// <summary>
        /// Find TransactionHistory by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<TransactionHistoryModel> FindByID(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        FindAllItemReponse<TransactionHistoryModel> FindByTransactionReference(long referenceId);

        /// <summary>
        /// Delete TransactionHistory by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create TransactionHistory
        /// </summary>
        /// <param name="TransactionHistory"></param>
        /// <returns></returns>
        InsertResponse Create(TransactionHistoryModel transaction);

        /// <summary>
        /// Get all TransactionHistory
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<TransactionHistoryModel> GetAlls();

        /// <summary>
        /// Find TransactionHistory by userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindAllItemReponse<TransactionHistoryModel> FindByUserID(string userID);
    }
}
