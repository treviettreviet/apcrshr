using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IUploadService
    {
        /// <summary>
        /// Create new upload
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        InsertResponse CreateUpload(UploadModel upload);

        /// <summary>
        /// Get upload by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<UploadModel> GetUploadByID(string id);

        /// <summary>
        /// Get all upload
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<UploadModel> GetUploads();

        /// <summary>
        /// Delete upload
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteUpload(string id);

        /// <summary>
        /// Update upload
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        BaseResponse UpdateUpload(UploadModel upload);
    }
}
