using AutoMapper;
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
    public class PhotoService : IPhotoService
    {

        public DataModel.Response.FindItemReponse<DataModel.Model.PhotoModel> FindPhotoByID(string id)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                Photo photo = photoRepository.FindByID(id);
                var _photo = MapperUtil.CreateMapper().Mapper.Map<Photo, PhotoModel>(photo);
                return new FindItemReponse<PhotoModel>
                {
                    Item = _photo,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<PhotoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.PhotoModel> FindPhotoByActionURL(string actionURL)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                Photo photo = photoRepository.FindByActionURL(actionURL);
                var _photo = MapperUtil.CreateMapper().Mapper.Map<Photo, PhotoModel>(photo);
                return new FindItemReponse<PhotoModel>
                {
                    Item = _photo,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<PhotoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public BaseResponse DeletePhoto(string id)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                photoRepository.Delete(id);
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

        public BaseResponse UpdatePhoto(PhotoModel photo)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                var _photo = MapperUtil.CreateMapper().Mapper.Map<PhotoModel, Photo>(photo);
                photoRepository.Update(_photo);
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

        public FindAllItemReponse<PhotoModel> GetPhoto()
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                IList<Photo> photo = photoRepository.FindAll();
                var _photo = photo.Select(n => MapperUtil.CreateMapper().Mapper.Map<Photo, PhotoModel>(n)).ToList();
                return new FindAllItemReponse<PhotoModel>
                {
                    Items = _photo,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PhotoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<PhotoModel> GetPhoto(int pageSize, int pageIndex)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                var result = photoRepository.FindAll(pageSize, pageIndex);
                var _photo = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Photo, PhotoModel>(n)).ToList();
                return new FindAllItemReponse<PhotoModel>
                {
                    Count = result.Item1,
                    Items = _photo,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PhotoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public InsertResponse CreatePhoto(PhotoModel photo)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                Photo _photo = MapperUtil.CreateMapper().Mapper.Map<PhotoModel, Photo>(photo);
                object id = photoRepository.Insert(_photo);
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

        public FindItemReponse<PhotoModel> GetPhotoByID(string id)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                Photo photo = photoRepository.FindByID(id);
                var _photo = MapperUtil.CreateMapper().Mapper.Map<Photo, PhotoModel>(photo);
                return new FindItemReponse<PhotoModel>
                {
                    Item = _photo,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<PhotoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<PhotoModel> GetPhotoByAlbum(string AlbumActionURL, int pageSize, int pageIndex)
        {
            try
            {
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();

                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();

                var album = albumRepository.FindByActionURL(AlbumActionURL);
                var result = photoRepository.FindByAlbum(album.AlbumID, pageSize, pageIndex);
                var _photos = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Photo, PhotoModel>(n)).ToList();
                return new FindAllItemReponse<PhotoModel>
                {
                    Count = result.Item1,
                    Items = _photos,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<PhotoModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
