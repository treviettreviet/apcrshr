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
    public class VideoService : IVideoService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.VideoModel> FindVideoByID(string id)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                Video video = videoRepository.FindByID(id);
                var _video = MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(video);
                return new FindItemReponse<VideoModel>
                {
                    Item = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.VideoModel> FindVideoByActionURL(string actionURL)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                Video video = videoRepository.FindByActionURL(actionURL);
                var _video = MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(video);
                return new FindItemReponse<VideoModel>
                {
                    Item = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeleteVideo(string id)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                videoRepository.Delete(id);
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

        public DataModel.Response.BaseResponse UpdateVideo(DataModel.Model.VideoModel video)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                var _video = MapperUtil.CreateMapper().Mapper.Map<VideoModel, Video>(video);
                videoRepository.Update(_video);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.VideoModel> GetVideo()
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                IList<Video> videos = videoRepository.FindAll();
                var _video = videos.Select(n => MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(n)).ToList();
                return new FindAllItemReponse<VideoModel>
                {
                    Items = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.VideoModel> GetVideo(int pageSize, int pageIndex)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                var result = videoRepository.FindAll(pageSize, pageIndex);
                var _video = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(n)).ToList();
                return new FindAllItemReponse<VideoModel>
                {
                    Count = result.Item1,
                    Items = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse CreateVideo(DataModel.Model.VideoModel video)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                Video _video = MapperUtil.CreateMapper().Mapper.Map<VideoModel, Video>(video);
                object id = videoRepository.Insert(_video);
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

        public DataModel.Response.FindItemReponse<DataModel.Model.VideoModel> GetVideoByID(string videoID)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                Video video = videoRepository.FindByID(videoID);
                var _video = MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(video);
                return new FindItemReponse<VideoModel>
                {
                    Item = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.VideoModel> GetRelatedVideo(DateTime date, int pageSize, int pageIndex, string language)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();

                var result = videoRepository.FindAllRelated(date, pageSize, pageIndex,language);
                var _video = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(n)).ToList();
                return new FindAllItemReponse<VideoModel>
                {
                    Count = result.Item1,
                    Items = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.VideoModel> GetTopVideo(int top, string language)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                IList<Video> videos = videoRepository.FindTop(top, language);
                var _videos = videos.Select(n => MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(n)).ToList();
                return new FindAllItemReponse<VideoModel>
                {
                    Items = _videos,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };

            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<VideoModel> GetVideo(int pageSize, int pageIndex, string language)
        {
            try
            {
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                var result = videoRepository.FindAll(pageSize, pageIndex, language);
                var _video = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Video, VideoModel>(n)).ToList();
                return new FindAllItemReponse<VideoModel>
                {
                    Count = result.Item1,
                    Items = _video,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<VideoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
