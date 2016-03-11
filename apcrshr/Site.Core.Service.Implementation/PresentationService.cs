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
    public class PresentationService : IPresentationService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.PresentationModel> FindPresentationByID(string id)
        {
            try
            {
                IPresentationRepository presentationRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                Presentation pre = presentationRepository.FindByID(id);
                var _pre = MapperUtil.CreateMapper().Mapper.Map<Presentation, PresentationModel>(pre);
                return new FindItemReponse<PresentationModel>
                {
                    Item = _pre,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<PresentationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.PresentationModel> FindPresentationByActionURL(string actionURL)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                Presentation pre = preRepository.FindByActionURL(actionURL);
                var _pre = MapperUtil.CreateMapper().Mapper.Map<Presentation, PresentationModel>(pre);
                return new FindItemReponse<PresentationModel>
                {
                    Item = _pre,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<PresentationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeletePresentation(string id)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                preRepository.Delete(id);
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

        public DataModel.Response.BaseResponse UpdatePresentation(DataModel.Model.PresentationModel presentation)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                var _pre = MapperUtil.CreateMapper().Mapper.Map<PresentationModel, Presentation>(presentation);
                preRepository.Update(_pre);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.PresentationModel> GetPresentation()
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                IList<Presentation> pres = preRepository.FindAll();
                var _pres = pres.Select(n => MapperUtil.CreateMapper().Mapper.Map<Presentation, PresentationModel>(n)).ToList();
                return new FindAllItemReponse<PresentationModel>
                {
                    Items = _pres,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PresentationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.PresentationModel> GetPresentation(int pageSize, int pageIndex)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                var result = preRepository.FindAll(pageSize, pageIndex);
                var _pre = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Presentation, PresentationModel>(n)).ToList();
                return new FindAllItemReponse<PresentationModel>
                {
                    Count = result.Item1,
                    Items = _pre,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PresentationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse CreatePresentation(DataModel.Model.PresentationModel presentation)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                Presentation _pre = MapperUtil.CreateMapper().Mapper.Map<PresentationModel, Presentation>(presentation);
                object id = preRepository.Insert(_pre);
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

        public DataModel.Response.FindItemReponse<DataModel.Model.PresentationModel> GetPresentationByID(string presentationID)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                Presentation pre = preRepository.FindByID(presentationID);
                var _pre = MapperUtil.CreateMapper().Mapper.Map<Presentation, PresentationModel>(pre);
                return new FindItemReponse<PresentationModel>
                {
                    Item = _pre,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<PresentationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.PresentationModel> GetRelatedPresentation(DateTime date, int pageSize, int pageIndex)
        {
            try
            {
                IPresentationRepository preRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();

                var result = preRepository.FindAllRelated(date, pageSize, pageIndex);
                var _pre = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Presentation, PresentationModel>(n)).ToList();
                return new FindAllItemReponse<PresentationModel>
                {
                    Count = result.Item1,
                    Items = _pre,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<PresentationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
