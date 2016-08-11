using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;

namespace Site.Core.Service.Contract
{
    public interface ITrainingService
    {
        /// <summary>
        /// Find training by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<TrainingModel> FindByID(string id);

        /// <summary>
        /// Delete training by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(string id);

        /// <summary>
        /// Create training
        /// </summary>
        /// <param name="training"></param>
        /// <returns></returns>
        InsertResponse Create(TrainingModel training);

        /// <summary>
        /// Update training
        /// </summary>
        /// <param name="training"></param>
        /// <returns></returns>
        BaseResponse Update(TrainingModel training);

        /// <summary>
        /// Get all training
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<TrainingModel> GetAlls();

        /// <summary>
        /// Get trainings by scholarship ID
        /// </summary>
        /// <param name="scholarship"></param>
        /// <returns></returns>
        FindAllItemReponse<TrainingModel> FindByscholarshipID(string scholarship);
    }
}
