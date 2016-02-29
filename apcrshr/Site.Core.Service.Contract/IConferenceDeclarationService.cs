using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IConferenceDeclarationService
    {
        /// <summary>
        /// Find ConferenceDeclaration by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<ConferenceDeclarationModel> FindConferenceByID(string id);

        /// <summary>
        /// Delete ConferenceDeclaration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteConference(string id);

        /// <summary>
        /// Update ConferenceDeclaration
        /// </summary>
        /// <param name="Conference"></param>
        /// <returns></returns>
        BaseResponse UpdateConference(ConferenceDeclarationModel conference);

        /// <summary>
        /// Get all ConferenceDeclaration
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<ConferenceDeclarationModel> GetConference();

        /// <summary>
        /// Get ConferenceDeclaration with paging
        /// </summary>
        /// <param name="pageSize">int</param>
        /// <param name="pageIndex">int</param>
        /// <returns></returns>
        FindAllItemReponse<ConferenceDeclarationModel> GetConference(int pageSize, int pageIndex);

        /// <summary>
        /// Create ConferenceDeclaration
        /// </summary>
        /// <param name="Conference"></param>
        /// <returns></returns>
        InsertResponse CreateConference(ConferenceDeclarationModel conference);

        /// <summary>
        /// Get ConferenceDeclaration by ID
        /// </summary>
        /// <param name="conferenceID"></param>
        /// <returns></returns>
        FindItemReponse<ConferenceDeclarationModel> GetConferenceByID(string conferenceID);
    }
}
