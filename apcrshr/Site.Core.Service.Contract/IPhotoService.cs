using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IPhotoService
    {
        /// <summary>
        /// Find Photo by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<PhotoModel> FindPhotoByID(string id);

        /// <summary>
        /// Get Photo detail
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<PhotoModel> FindPhotoByActionURL(string actionURL);

        /// <summary>
        /// Delete Photo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeletePhoto(string id);

        /// <summary>
        /// Update Photo
        /// </summary>
        /// <param name="Photo"></param>
        /// <returns></returns>
        BaseResponse UpdatePhoto(PhotoModel photo);

        /// <summary>
        /// Get all Photo
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<PhotoModel> GetPhoto();

        /// <summary>
        /// Get ConferenceDeclaration with paging
        /// </summary>
        /// <param name="pageSize">int</param>
        /// <param name="pageIndex">int</param>
        /// <returns></returns>
        FindAllItemReponse<PhotoModel> GetPhoto(int pageSize, int pageIndex);

        /// <summary>
        /// Create Photo
        /// </summary>
        /// <param name="Photo"></param>
        /// <returns></returns>
        InsertResponse CreatePhoto(PhotoModel photo);

        /// <summary>
        /// Get Photo by ID
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <returns></returns>
        FindItemReponse<PhotoModel> GetPhotoByID(string id);

        /// <summary>
        /// Get all related Photo by date
        /// </summary>
        /// <param name="category"></param>
        /// <param name="AlbumID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        FindAllItemReponse<PhotoModel> GetPhotoByAlbum(string AlbumActionURL, int pageSize, int pageIndex);
    }
}
