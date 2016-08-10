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
    public class EducationService : IEducationService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.EducationModel> FindByID(string id)
        {
            try
            {
                IEducationRepository educationRepository = RepositoryClassFactory.GetInstance().GetEducationRepository();
                Education education = educationRepository.FindByID(id);
                var _education = MapperUtil.CreateMapper().Mapper.Map<Education, EducationModel>(education);
                return new FindItemReponse<EducationModel>
                {
                    Item = _education,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<EducationModel>
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
                IEducationRepository educationRepository = RepositoryClassFactory.GetInstance().GetEducationRepository();
                educationRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.EducationModel education)
        {
            try
            {
                IEducationRepository educationRepository = RepositoryClassFactory.GetInstance().GetEducationRepository();
                IList<Education> _educations = educationRepository.FindByMainCourseStudy(education.MainCourseStudy);
                if (_educations != null && _educations.Count > 0)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, "Education", education.MainCourseStudy)
                    };
                }
                var _education = MapperUtil.CreateMapper().Mapper.Map<EducationModel, Education>(education);
                object id = educationRepository.Insert(_education);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.EducationModel> GetAlls()
        {
            try
            {
                IEducationRepository educationRepository = RepositoryClassFactory.GetInstance().GetEducationRepository();
                IList<Education> educations = educationRepository.FindAll();
                var _educations = educations.Select(n => MapperUtil.CreateMapper().Mapper.Map<Education, EducationModel>(n)).ToList();
                return new FindAllItemReponse<EducationModel>
                {
                    Items = _educations,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<EducationModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public BaseResponse Update(EducationModel education)
        {
            try
            {
                IEducationRepository educationRepository = RepositoryClassFactory.GetInstance().GetEducationRepository();
                var _education = MapperUtil.CreateMapper().Mapper.Map<EducationModel, Education>(education);
                educationRepository.Update(_education);
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
    }
}
