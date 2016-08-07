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
    public class YouthScholarshipService : IYouthScholarshipService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.YouthScholarshipModel> FindByID(string id)
        {
            try
            {
                IYouthScholarshipRepository youthScholarshipRepository = RepositoryClassFactory.GetInstance().GetYouthShcolarshipReoisitory();
                YouthScholarship scholarship = youthScholarshipRepository.FindByID(id);
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<YouthScholarship, YouthScholarshipModel>(scholarship);
                return new FindItemReponse<YouthScholarshipModel>
                {
                    Item = _scholarship,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<YouthScholarshipModel>
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
                IYouthScholarshipRepository youthScholarshipRepository = RepositoryClassFactory.GetInstance().GetYouthShcolarshipReoisitory();
                youthScholarshipRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.YouthScholarshipModel scholarship)
        {
            try
            {
                IYouthScholarshipRepository youthScholarshipRepository = RepositoryClassFactory.GetInstance().GetYouthShcolarshipReoisitory();
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<YouthScholarshipModel, YouthScholarship>(scholarship);
                object id = youthScholarshipRepository.Insert(_scholarship);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.YouthScholarshipModel scholarship)
        {
            try
            {
                IYouthScholarshipRepository youthScholarshipRepository = RepositoryClassFactory.GetInstance().GetYouthShcolarshipReoisitory();
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<YouthScholarshipModel, YouthScholarship>(scholarship);
                youthScholarshipRepository.Update(_scholarship);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.YouthScholarshipModel> GetAlls()
        {
            try
            {
                IYouthScholarshipRepository youthScholarshipRepository = RepositoryClassFactory.GetInstance().GetYouthShcolarshipReoisitory();
                IList<YouthScholarship> scholarships = youthScholarshipRepository.FindAll();
                var _scholarships = scholarships.Select(n => MapperUtil.CreateMapper().Mapper.Map<YouthScholarship, YouthScholarshipModel>(n)).ToList();
                return new FindAllItemReponse<YouthScholarshipModel>
                {
                    Items = _scholarships,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<YouthScholarshipModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindItemReponse<YouthScholarshipModel> FindByUserID(string userID)
        {
            try
            {
                IYouthScholarshipRepository youthScholarshipRepository = RepositoryClassFactory.GetInstance().GetYouthShcolarshipReoisitory();
                YouthScholarship scholarship = youthScholarshipRepository.FindByUserID(userID);
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<YouthScholarship, YouthScholarshipModel>(scholarship);
                return new FindItemReponse<YouthScholarshipModel>
                {
                    Item = _scholarship,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<YouthScholarshipModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
