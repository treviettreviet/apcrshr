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
    public class UserSubmissionService : IUserSubmissionService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.UserSubmissionModel> FindByID(string id)
        {
            try
            {
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                UserSubmission submission = submissionRepository.FindByID(id);
                var _submission = MapperUtil.CreateMapper().Mapper.Map<UserSubmission, UserSubmissionModel>(submission);
                return new FindItemReponse<UserSubmissionModel>
                {
                    Item = _submission,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<UserSubmissionModel>
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
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                submissionRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.UserSubmissionModel submission)
        {
            try
            {
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                var item = submissionRepository.FindBySubmissionNumber(submission.SubmissionNumber);
                if (item != null)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, "The submission number", submission.SubmissionNumber)
                    };
                }
                var _submission = MapperUtil.CreateMapper().Mapper.Map<UserSubmissionModel, UserSubmission>(submission);
                object id = submissionRepository.Insert(_submission);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.UserSubmissionModel submission)
        {
            try
            {
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                var _submission = MapperUtil.CreateMapper().Mapper.Map<UserSubmissionModel, UserSubmission>(submission);
                submissionRepository.Update(_submission);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.UserSubmissionModel> GetAlls()
        {
            try
            {
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                IList<UserSubmission> submissions = submissionRepository.FindAll();
                var _submissions = submissions.Select(n => MapperUtil.CreateMapper().Mapper.Map<UserSubmission, UserSubmissionModel>(n)).ToList();
                return new FindAllItemReponse<UserSubmissionModel>
                {
                    Items = _submissions,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<UserSubmissionModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<UserSubmissionModel> FindByUserID(string userID)
        {
            try
            {
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                IList<UserSubmission> submissions = submissionRepository.FindByUserID(userID);
                var _submissions = submissions.Select(n => MapperUtil.CreateMapper().Mapper.Map<UserSubmission, UserSubmissionModel>(n)).ToList();
                return new FindAllItemReponse<UserSubmissionModel>
                {
                    Items = _submissions,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<UserSubmissionModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindItemReponse<UserSubmissionModel> FindBySubmissionNumber(string submissionNumber)
        {
            try
            {
                IUserSubmissionRepository submissionRepository = RepositoryClassFactory.GetInstance().GetUserSubmissionRepository();
                UserSubmission submission = submissionRepository.FindBySubmissionNumber(submissionNumber);
                var _submission = MapperUtil.CreateMapper().Mapper.Map<UserSubmission, UserSubmissionModel>(submission);
                return new FindItemReponse<UserSubmissionModel>
                {
                    Item = _submission,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<UserSubmissionModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
