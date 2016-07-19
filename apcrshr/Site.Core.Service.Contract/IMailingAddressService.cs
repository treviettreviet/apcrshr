using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface IMailingAddressService
    {
        /// <summary>
        /// Create new mailing address
        /// </summary>
        /// <param name="mailing"></param>
        /// <returns></returns>
        InsertResponse CreateMailingAddress(MailingAddressModel mailing);

        /// <summary>
        /// Find mailing address by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        FindItemReponse<MailingAddressModel> FindMailingAddressByID(string Id);

        /// <summary>
        /// Find mailing address by user ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        FindAllItemReponse<MailingAddressModel> FindMailingAddressByUser(string userID);

        /// <summary>
        /// Delete mailing address by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteMailingAddress(string id);

        /// <summary>
        /// Update mailing address
        /// </summary>
        /// <param name="mailing"></param>
        /// <returns></returns>
        BaseResponse UpdateMailingAddress(MailingAddressModel mailing);

        /// <summary>
        /// Get all mailing addresses
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<MailingAddressModel> GetMailingAddresses();

        /// <summary>
        /// Find mailing by activation code
        /// </summary>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        FindAllItemReponse<MailingAddressModel> GetMailingAddresses(string activationCode);
    }
}
