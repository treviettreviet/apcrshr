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
    public class LogisticSheduleService : ILogisticSheduleService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.LogisticScheduleModel> FindByID(string id)
        {
            try
            {
                ILogisticSheduleRepository logisticRepository = RepositoryClassFactory.GetInstance().GetLogisticRepository();
                LogisticSchedule logistic = logisticRepository.FindByID(id);
                var _logistic = MapperUtil.CreateMapper().Mapper.Map<LogisticSchedule, LogisticScheduleModel>(logistic);
                return new FindItemReponse<LogisticScheduleModel>
                {
                    Item = _logistic,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<LogisticScheduleModel>
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
                ILogisticSheduleRepository logisticRepository = RepositoryClassFactory.GetInstance().GetLogisticRepository();
                logisticRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.LogisticScheduleModel logistic)
        {
            try
            {
                ILogisticSheduleRepository logisticRepository = RepositoryClassFactory.GetInstance().GetLogisticRepository();
                object id = logisticRepository.Insert(MapperUtil.CreateMapper().Mapper.Map<LogisticScheduleModel, LogisticSchedule>(logistic));
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.LogisticScheduleModel logistic)
        {
            try
            {
                ILogisticSheduleRepository logisticRepository = RepositoryClassFactory.GetInstance().GetLogisticRepository();
                LogisticSchedule _logistic = MapperUtil.CreateMapper().Mapper.Map<LogisticScheduleModel, LogisticSchedule>(logistic);
                logisticRepository.Update(_logistic);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.LogisticScheduleModel> GetAlls()
        {
            try
            {
                ILogisticSheduleRepository logisticRepository = RepositoryClassFactory.GetInstance().GetLogisticRepository();
                IList<LogisticSchedule> logistics = logisticRepository.FindAll();
                var _logistics = logistics.Select(n => MapperUtil.CreateMapper().Mapper.Map<LogisticSchedule, LogisticScheduleModel>(n)).ToList();
                return new FindAllItemReponse<LogisticScheduleModel>
                {
                    Items = _logistics,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<LogisticScheduleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindItemReponse<LogisticScheduleModel> FindByUserID(string userID)
        {
            try
            {
                ILogisticSheduleRepository logisticRepository = RepositoryClassFactory.GetInstance().GetLogisticRepository();
                LogisticSchedule logistic = logisticRepository.FindByUserID(userID);
                var _logistic = MapperUtil.CreateMapper().Mapper.Map<LogisticSchedule, LogisticScheduleModel>(logistic);
                return new FindItemReponse<LogisticScheduleModel>
                {
                    Item = _logistic,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<LogisticScheduleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
