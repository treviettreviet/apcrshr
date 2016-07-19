using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation.ModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Implementation
{
    public class SessionService : ISessionService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.SessionModel> FindID(string id)
        {
            try
            {
                ISessionRepository sessionRepository = RepositoryClassFactory.GetInstance().GetSessionRepository();
                Session session = sessionRepository.FindByID(id);
                var _session = MapperUtil.CreateMapper().Mapper.Map<Session, SessionModel>(session);
                return new FindItemReponse<SessionModel>
                {
                    Item = _session,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<SessionModel>
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
                ISessionRepository sessionRepository = RepositoryClassFactory.GetInstance().GetSessionRepository();
                sessionRepository.Delete(id);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.SessionModel session)
        {
            try
            {
                ISessionRepository sessionRepository = RepositoryClassFactory.GetInstance().GetSessionRepository();
                Session _session = MapperUtil.CreateMapper().Mapper.Map<SessionModel, Session>(session);
                sessionRepository.Update(_session);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.SessionModel session)
        {
            try
            {
                ISessionRepository sessionRepository = RepositoryClassFactory.GetInstance().GetSessionRepository();
                object id = sessionRepository.Insert(MapperUtil.CreateMapper().Mapper.Map<SessionModel, Session>(session));
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.SessionModel> GetSessions()
        {
            try
            {
                ISessionRepository sessionRepository = RepositoryClassFactory.GetInstance().GetSessionRepository();
                IList<Session> sessions = sessionRepository.FindAll();
                var _sessions = sessions.Select(n => MapperUtil.CreateMapper().Mapper.Map<Session, SessionModel>(n)).ToList();
                return new FindAllItemReponse<SessionModel>
                {
                    Items = _sessions,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<SessionModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
