using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface ISubscriberService
    {
        /// <summary>
        /// Create a new subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        InsertResponse CreateSubscriber(SubscriberModel subscriber);

        /// <summary>
        /// Delete a subscriber
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteSubscriber(string id);

        /// <summary>
        /// Update a subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        BaseResponse UpdateSubscriber(SubscriberModel subscriber);

        /// <summary>
        /// Get all subscribers
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<SubscriberModel> GetSubscribers();

        /// <summary>
        /// Find subscriber by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<SubscriberModel> FindSubscriberByID(string id);

        /// <summary>
        /// Find subscriber by email
        /// </summary>
        /// <param name="email">string</param>
        /// <returns></returns>
        FindItemReponse<SubscriberModel> FindSubscriberByEmail(string email);
    }
}
