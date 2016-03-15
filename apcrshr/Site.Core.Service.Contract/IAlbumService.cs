using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IAlbumService
    {
        /// <summary>
        /// Find Album by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<AlbumModel> FindAlbumByID(string id);

        /// <summary>
        /// Get Album detail
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<AlbumModel> FindAlbumByActionURL(string actionURL);

        /// <summary>
        /// Delete Album
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteAlbum(string id);

        /// <summary>
        /// Update Album
        /// </summary>
        /// <param name="Album"></param>
        /// <returns></returns>
        BaseResponse UpdateAlbum(AlbumModel album);

        /// <summary>
        /// Create Album
        /// </summary>
        /// <param name="Album"></param>
        /// <returns></returns>
        InsertResponse CreateAlbum(AlbumModel album);

        /// <summary>
        /// Get all Album
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<AlbumModel> GetAlbum();
    }
}
