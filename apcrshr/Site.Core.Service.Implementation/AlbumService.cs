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
    public class AlbumService : IAlbumService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.AlbumModel> FindAlbumByID(string id)
        {
            try
            {
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                Album album = albumRepository.FindByID(id);
                var _album = MapperUtil.CreateMapper().Mapper.Map<Album, AlbumModel>(album);
                return new FindItemReponse<AlbumModel>
                {
                    Item = _album,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<AlbumModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.AlbumModel> FindAlbumByActionURL(string actionURL)
        {
            try
            {
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                Album album = albumRepository.FindByActionURL(actionURL);
                var _album = MapperUtil.CreateMapper().Mapper.Map<Album, AlbumModel>(album);
                return new FindItemReponse<AlbumModel>
                {
                    Item = _album,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<AlbumModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeleteAlbum(string id)
        {
            try
            {
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                albumRepository.Delete(id);
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

        public DataModel.Response.BaseResponse UpdateAlbum(DataModel.Model.AlbumModel album)
        {
            try
            {
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                var _album = MapperUtil.CreateMapper().Mapper.Map<AlbumModel, Album>(album);
                albumRepository.Update(_album);
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

        public DataModel.Response.InsertResponse CreateAlbum(DataModel.Model.AlbumModel album)
        {
            try
            {
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                Album _album = MapperUtil.CreateMapper().Mapper.Map<AlbumModel, Album>(album);
                object id = albumRepository.Insert(_album);
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

        public FindAllItemReponse<AlbumModel> GetAlbum()
        {
            try
            {
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                IList<Album> albums = albumRepository.FindAll();
                var _albums = albums.Select(n => MapperUtil.CreateMapper().Mapper.Map<Album, AlbumModel>(n)).ToList();
                return new FindAllItemReponse<AlbumModel>
                {
                    Items = _albums,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<AlbumModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
