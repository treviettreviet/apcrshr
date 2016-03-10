using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IVideoService
    {
        /// <summary>
        /// Find Video by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<VideoModel> FindVideoByID(string id);

        /// <summary>
        /// Get Video detail
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<VideoModel> FindVideoByActionURL(string actionURL);

        /// <summary>
        /// Delete Video
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteVideo(string id);

        /// <summary>
        /// Update Video
        /// </summary>
        /// <param name="Video"></param>
        /// <returns></returns>
        BaseResponse UpdateVideo(VideoModel video);

        /// <summary>
        /// Get all Videos
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<VideoModel> GetVideo();

        /// <summary>
        /// Get Video with paging
        /// </summary>
        /// <param name="pageSize">int</param>
        /// <param name="pageIndex">int</param>
        /// <returns></returns>
        FindAllItemReponse<VideoModel> GetVideo(int pageSize, int pageIndex);

        /// <summary>
        /// Get video with pagging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        FindAllItemReponse<VideoModel> GetVideo(int pageSize, int pageIndex, string language);

        /// <summary>
        /// Create Video
        /// </summary>
        /// <param name="Video"></param>
        /// <returns></returns>
        InsertResponse CreateVideo(VideoModel video);

        /// <summary>
        /// Get Video by ID
        /// </summary>
        /// <param name="VideoID"></param>
        /// <returns></returns>
        FindItemReponse<VideoModel> GetVideoByID(string videoID);

        /// <summary>
        /// Get all related Video by date
        /// </summary>
        /// <param name="category"></param>
        /// <param name="date"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// /// <param name="language"></param>
        /// <returns></returns>
        FindAllItemReponse<VideoModel> GetRelatedVideo(DateTime date, int pageSize, int pageIndex, string language);
        
        /// <summary>
        /// Get top Video
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        FindAllItemReponse<VideoModel> GetTopVideo(int top, string language);
    }
}
