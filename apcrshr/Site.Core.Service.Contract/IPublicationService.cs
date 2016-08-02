using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IPublicationService
    {
        /// <summary>
        /// Find publication by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<PublicationModel> FindByID(string id);

        /// <summary>
        /// Delete publication by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create publication
        /// </summary>
        /// <param name="publication"></param>
        /// <returns></returns>
        InsertResponse Create(PublicationModel publication);

        /// <summary>
        /// Update publication
        /// </summary>
        /// <param name="publication"></param>
        /// <returns></returns>
        BaseResponse Update(PublicationModel publication);

        /// <summary>
        /// Get all publication
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<PublicationModel> GetAlls();
    }
}
