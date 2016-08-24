using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface IPaymentService
    {
        /// <summary>
        /// Find payment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<PaymentModel> FindByID(string id);

        /// <summary>
        /// Delete payment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        InsertResponse Create(PaymentModel payment);

        /// <summary>
        /// Update payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        BaseResponse Update(PaymentModel payment);

        /// <summary>
        /// Get all payment
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<PaymentModel> GetAlls();
    }
}
