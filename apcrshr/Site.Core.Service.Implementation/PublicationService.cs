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
    public class PublicationService : IPublicationService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.PublicationModel> FindByID(string id)
        {
            try
            {
                IPublicationRepository publicationRepository = RepositoryClassFactory.GetInstance().GetPublicationRepository();
                Publication publication = publicationRepository.FindByID(id);
                var _publication = MapperUtil.CreateMapper().Mapper.Map<Publication, PublicationModel>(publication);
                return new FindItemReponse<PublicationModel>
                {
                    Item = _publication,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<PublicationModel>
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
                IPublicationRepository publicationRepository = RepositoryClassFactory.GetInstance().GetPublicationRepository();
                publicationRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.PublicationModel publication)
        {
            try
            {
                IPublicationRepository publicationRepository = RepositoryClassFactory.GetInstance().GetPublicationRepository();
                IList<Publication> _publications = publicationRepository.FindByTitle(publication.Title);
                if (_publications != null && _publications.Count > 0)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, "Publication", publication.Title)
                    };
                }
                var _publication = MapperUtil.CreateMapper().Mapper.Map<PublicationModel, Publication>(publication);
                object id = publicationRepository.Insert(_publication);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.PublicationModel publication)
        {
            try
            {
                IPublicationRepository publicationRepository = RepositoryClassFactory.GetInstance().GetPublicationRepository();
                var _publication = MapperUtil.CreateMapper().Mapper.Map<PublicationModel, Publication>(publication);
                publicationRepository.Update(_publication);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.PublicationModel> GetAlls()
        {
            try
            {
                IPublicationRepository publicationRepository = RepositoryClassFactory.GetInstance().GetPublicationRepository();
                IList<Publication> publications = publicationRepository.FindAll();
                var _publications = publications.Select(n => MapperUtil.CreateMapper().Mapper.Map<Publication, PublicationModel>(n)).ToList();
                return new FindAllItemReponse<PublicationModel>
                {
                    Items = _publications,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PublicationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<PublicationModel> FindByscholarshipID(string scholarship)
        {
            try
            {
                IPublicationRepository publicationRepository = RepositoryClassFactory.GetInstance().GetPublicationRepository();
                IList<Publication> publications = publicationRepository.FindByYouthScholarshipID(scholarship);
                var _publications = publications.Select(n => MapperUtil.CreateMapper().Mapper.Map<Publication, PublicationModel>(n)).ToList();
                return new FindAllItemReponse<PublicationModel>
                {
                    Items = _publications,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PublicationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
