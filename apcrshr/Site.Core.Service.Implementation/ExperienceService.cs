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
    public class ExperienceService : IExperienceService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.ExperienceModel> FindByID(string id)
        {
            try
            {
                IExperienceRepository experienceRepository = RepositoryClassFactory.GetInstance().GetExperienceRepository();
                Experience experience = experienceRepository.FindByID(id);
                var _experience = MapperUtil.CreateMapper().Mapper.Map<Experience, ExperienceModel>(experience);
                return new FindItemReponse<ExperienceModel>
                {
                    Item = _experience,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<ExperienceModel>
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
                IExperienceRepository experienceRepository = RepositoryClassFactory.GetInstance().GetExperienceRepository();
                experienceRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.ExperienceModel experience)
        {
            try
            {
                IExperienceRepository experienceRepository = RepositoryClassFactory.GetInstance().GetExperienceRepository();
                IList<Experience> _experiences = experienceRepository.FindByOrganization(experience.Organization);
                if (_experiences != null && _experiences.Count > 0)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, "Organization", experience.Organization)
                    };
                }
                var _experience = MapperUtil.CreateMapper().Mapper.Map<ExperienceModel, Experience>(experience);
                object id = experienceRepository.Insert(_experience);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.ExperienceModel experience)
        {
            try
            {
                IExperienceRepository experienceRepository = RepositoryClassFactory.GetInstance().GetExperienceRepository();
                var _experience = MapperUtil.CreateMapper().Mapper.Map<ExperienceModel, Experience>(experience);
                experienceRepository.Update(_experience);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ExperienceModel> GetAlls()
        {
            try
            {
                IExperienceRepository experienceRepository = RepositoryClassFactory.GetInstance().GetExperienceRepository();
                IList<Experience> experiences = experienceRepository.FindAll();
                var _experiences = experiences.Select(n => MapperUtil.CreateMapper().Mapper.Map<Experience, ExperienceModel>(n)).ToList();
                return new FindAllItemReponse<ExperienceModel>
                {
                    Items = _experiences,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<ExperienceModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<ExperienceModel> FindByscholarshipID(string scholarship)
        {
            try
            {
                IExperienceRepository experienceRepository = RepositoryClassFactory.GetInstance().GetExperienceRepository();
                IList<Experience> experiences = experienceRepository.FindByYouthScholarshipID(scholarship);
                var _experiences = experiences.Select(n => MapperUtil.CreateMapper().Mapper.Map<Experience, ExperienceModel>(n)).ToList();
                return new FindAllItemReponse<ExperienceModel>
                {
                    Items = _experiences,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<ExperienceModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
