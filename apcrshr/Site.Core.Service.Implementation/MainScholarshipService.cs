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
    public class MainScholarshipService : IMainScholarshipService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.MainScholarshipModel> FindByID(string id)
        {
            try
            {
                IMainScholarshipRepository mainScholarshipRepository = RepositoryClassFactory.GetInstance().GetMainScholarshipRepository();
                MainScholarship scholarship = mainScholarshipRepository.FindByID(id);
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<MainScholarship, MainScholarshipModel>(scholarship);
                return new FindItemReponse<MainScholarshipModel>
                {
                    Item = _scholarship,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<MainScholarshipModel>
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
                IMainScholarshipRepository mainScholarshipRepository = RepositoryClassFactory.GetInstance().GetMainScholarshipRepository();
                mainScholarshipRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.MainScholarshipModel scholarship)
        {
            try
            {
                IMainScholarshipRepository mainScholarshipRepository = RepositoryClassFactory.GetInstance().GetMainScholarshipRepository();
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<MainScholarshipModel, MainScholarship>(scholarship);
                object id = mainScholarshipRepository.Insert(_scholarship);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.MainScholarshipModel scholarship)
        {
            try
            {
                IMainScholarshipRepository mainScholarshipRepository = RepositoryClassFactory.GetInstance().GetMainScholarshipRepository();
                var _scholarship = MapperUtil.CreateMapper().Mapper.Map<MainScholarshipModel, MainScholarship>(scholarship);
                mainScholarshipRepository.Update(_scholarship);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.MainScholarshipModel> GetAlls()
        {
            try
            {
                IMainScholarshipRepository mainScholarshipRepository = RepositoryClassFactory.GetInstance().GetMainScholarshipRepository();
                IList<MainScholarship> scholarships = mainScholarshipRepository.FindAll();
                var _scholarships = scholarships.Select(n => MapperUtil.CreateMapper().Mapper.Map<MainScholarship, MainScholarshipModel>(n)).ToList();
                return new FindAllItemReponse<MainScholarshipModel>
                {
                    Items = _scholarships,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<MainScholarshipModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
