using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface ISessionService
    {
        /// <summary>
        /// Find session by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<SessionModel> FindID(string id);

        /// <summary>
        /// Delete session by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Update session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        BaseResponse Update(SessionModel session);

        /// <summary>
        /// Create session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        InsertResponse Create(SessionModel session);

        /// <summary>
        /// Get all sessions
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<SessionModel> GetSessions();
    }
}
