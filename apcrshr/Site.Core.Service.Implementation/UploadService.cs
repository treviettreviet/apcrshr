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
    public class UploadService : IUploadService
    {
        public DataModel.Response.InsertResponse CreateUpload(DataModel.Model.UploadModel upload)
        {
            try
            {
                IUploadRepository uploadRepository = RepositoryClassFactory.GetInstance().GetUploadRepository();
                Upload _upload = MapperUtil.CreateMapper().Mapper.Map<UploadModel, Upload>(upload);
                object id = uploadRepository.Insert(_upload);
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

        public DataModel.Response.FindItemReponse<DataModel.Model.UploadModel> GetUploadByID(string id)
        {
            try
            {
                IUploadRepository uploadRepository = RepositoryClassFactory.GetInstance().GetUploadRepository();
                Upload upload = uploadRepository.FindByID(id);
                var _upload = MapperUtil.CreateMapper().Mapper.Map<Upload, UploadModel>(upload);
                return new FindItemReponse<UploadModel>
                {
                    Item = _upload,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<UploadModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.UploadModel> GetUploads()
        {
            try
            {
                IUploadRepository uploadRepository = RepositoryClassFactory.GetInstance().GetUploadRepository();
                IList<Upload> uploads = uploadRepository.FindAll();
                var _uploads = uploads.Select(a => MapperUtil.CreateMapper().Mapper.Map<Upload, UploadModel>(a)).ToList();
                return new FindAllItemReponse<UploadModel>
                {
                    Items = _uploads,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<UploadModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeleteUpload(string id)
        {
            try
            {
                IUploadRepository uploadRepository = RepositoryClassFactory.GetInstance().GetUploadRepository();
                uploadRepository.Delete(id);
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

        public DataModel.Response.BaseResponse UpdateUpload(DataModel.Model.UploadModel upload)
        {
            try
            {
                IUploadRepository uploadRepository = RepositoryClassFactory.GetInstance().GetUploadRepository();
                Upload _upload = MapperUtil.CreateMapper().Mapper.Map<UploadModel, Upload>(upload);
                uploadRepository.Update(_upload);
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


        public FindAllItemReponse<UploadModel> GetTopUploads(int top)
        {
            try
            {
                IUploadRepository uploadRepository = RepositoryClassFactory.GetInstance().GetUploadRepository();
                IList<Upload> uploads = uploadRepository.FindAll(top);
                var _uploads = uploads.Select(a => MapperUtil.CreateMapper().Mapper.Map<Upload, UploadModel>(a)).ToList();
                return new FindAllItemReponse<UploadModel>
                {
                    Items = _uploads,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<UploadModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
