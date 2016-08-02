using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation.ModelMapper;

namespace Site.Core.Service.Implementation
{
    public class TrainingService : ITrainingService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.TrainingModel> FindByID(string id)
        {
            try
            {
                ITrainingRepository trainingRepository = RepositoryClassFactory.GetInstance().GetTrainingRepository();
                Training training = trainingRepository.FindByID(id);
                var _training = MapperUtil.CreateMapper().Mapper.Map<Training, TrainingModel>(training);
                return new FindItemReponse<TrainingModel>
                {
                    Item = _training,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<TrainingModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse Delete(string id)
        {
            try
            {
                ITrainingRepository trainingRepository = RepositoryClassFactory.GetInstance().GetTrainingRepository();
                trainingRepository.Delete(id);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_delete_success
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse Create(DataModel.Model.TrainingModel training)
        {
            try
            {
                ITrainingRepository trainingRepository = RepositoryClassFactory.GetInstance().GetTrainingRepository();
                var _training = MapperUtil.CreateMapper().Mapper.Map<TrainingModel, Training>(training);
                object id = trainingRepository.Insert(_training);
                return new InsertResponse
                {
                    InsertID = id.ToString(),
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_create_success
                };
            }
            catch (Exception ex)
            {

                return new InsertResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse Update(DataModel.Model.TrainingModel training)
        {
            try
            {
                ITrainingRepository trainingRepository = RepositoryClassFactory.GetInstance().GetTrainingRepository();
                var _training = MapperUtil.CreateMapper().Mapper.Map<TrainingModel, Training>(training);
                trainingRepository.Update(_training);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_update_success
                };


            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.TrainingModel> GetAlls()
        {
            try
            {
                ITrainingRepository trainingRepository = RepositoryClassFactory.GetInstance().GetTrainingRepository();
                IList<Training> trainings = trainingRepository.FindAll();
                var _trainings = trainings.Select(n => MapperUtil.CreateMapper().Mapper.Map<Training, TrainingModel>(n)).ToList();
                return new FindAllItemReponse<TrainingModel>
                {
                    Items = _trainings,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<TrainingModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
