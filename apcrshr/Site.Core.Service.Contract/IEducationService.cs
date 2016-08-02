using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IEducationService
    {
        /// <summary>
        /// Find education by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<EducationModel> FindByID(string id);

        /// <summary>
        /// Delete education by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create education
        /// </summary>
        /// <param name="education"></param>
        /// <returns></returns>
        InsertResponse Create(EducationModel education);

        /// <summary>
        /// Update education
        /// </summary>
        /// <param name="education"></param>
        /// <returns></returns>
        BaseResponse Update(EducationModel education);

        /// <summary>
        /// Get all education
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<EducationModel> GetAlls();
    }
}
