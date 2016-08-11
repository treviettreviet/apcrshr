using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface ILeaderShipService
    {
        /// <summary>
        /// Find leadership by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<LeaderShipModel> FindByID(string id);

        /// <summary>
        /// Delete leadership by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create leadership
        /// </summary>
        /// <param name="leadership"></param>
        /// <returns></returns>
        InsertResponse Create(LeaderShipModel leadership);

        /// <summary>
        /// Update leadership
        /// </summary>
        /// <param name="leadership"></param>
        /// <returns></returns>
        BaseResponse Update(LeaderShipModel leadership);

        /// <summary>
        /// Get all leadership
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<LeaderShipModel> GetAlls();

        /// <summary>
        /// Get leadership by scholarship ID
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        FindAllItemReponse<LeaderShipModel> FindByscholarshipID(string scholarship);
    }
}
