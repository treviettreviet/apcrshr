using Site.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Implementation.ModelMapper;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.DataModel.Enum;

namespace Site.Core.Service.Implementation
{
    public class ConferenceDeclarationService : IConferenceDeclarationService
    {
        public FindItemReponse<DataModel.Model.ConferenceDeclarationModel> FindConferenceByID(string id)
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                ConferenceDeclaration con = conferenceRepository.FindByID(id);
                var _con = MapperUtil.CreateMapper().Mapper.Map<ConferenceDeclaration, ConferenceDeclarationModel>(con);
                return new FindItemReponse<ConferenceDeclarationModel>
                {
                    Item = _con,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<ConferenceDeclarationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeleteConference(string id)
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                conferenceRepository.Delete(id);
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

        public DataModel.Response.BaseResponse UpdateConference(DataModel.Model.ConferenceDeclarationModel conference)
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                var _con = MapperUtil.CreateMapper().Mapper.Map<ConferenceDeclarationModel, ConferenceDeclaration>(conference);
                conferenceRepository.Update(_con);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ConferenceDeclarationModel> GetConference()
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                IList<ConferenceDeclaration> cons = conferenceRepository.FindAll();
                var _con = cons.Select(n => MapperUtil.CreateMapper().Mapper.Map<ConferenceDeclaration, ConferenceDeclarationModel>(n)).ToList();
                return new FindAllItemReponse<ConferenceDeclarationModel>
                {
                    Items = _con,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<ConferenceDeclarationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ConferenceDeclarationModel> GetConference(int pageSize, int pageIndex)
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                var result = conferenceRepository.FindAll(pageSize,pageIndex);
                var _con = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<ConferenceDeclaration, ConferenceDeclarationModel>(n)).ToList();
                return new FindAllItemReponse<ConferenceDeclarationModel>
                {
                    Count = result.Item1,
                    Items = _con,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<ConferenceDeclarationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse CreateConference(DataModel.Model.ConferenceDeclarationModel conference)
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                var _con = MapperUtil.CreateMapper().Mapper.Map<ConferenceDeclarationModel, ConferenceDeclaration>(conference);
                object id = conferenceRepository.Insert(_con);
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

        public DataModel.Response.FindItemReponse<DataModel.Model.ConferenceDeclarationModel> GetConferenceByID(string conferenceID)
        {
            try
            {
                IConferenceDeclarationRepository conferenceRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                ConferenceDeclaration con = conferenceRepository.FindByID(conferenceID);
                var _con = MapperUtil.CreateMapper().Mapper.Map<ConferenceDeclaration, ConferenceDeclarationModel>(con);
                return new FindItemReponse<ConferenceDeclarationModel>
                {
                    Item = _con,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<ConferenceDeclarationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
