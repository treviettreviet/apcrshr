using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IExperienceService
    {
        /// <summary>
        /// Find experience by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<ExperienceModel> FindByID(string id);

        /// <summary>
        /// Delete experience by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create experience
        /// </summary>
        /// <param name="experience"></param>
        /// <returns></returns>
        InsertResponse Create(ExperienceModel experience);

        /// <summary>
        /// Update experience
        /// </summary>
        /// <param name="experience"></param>
        /// <returns></returns>
        BaseResponse Update(ExperienceModel experience);

        /// <summary>
        /// Get all experiences
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<ExperienceModel> GetAlls();

        /// <summary>
        /// Get experiences by scholarship ID
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        FindAllItemReponse<ExperienceModel> FindByscholarshipID(string scholarship);
    }
}
