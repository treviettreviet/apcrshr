using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;

namespace Site.Core.Service.Implementation
{
    public class NewsService : INewsService
    {
        public FindAllItemReponse<NewsModel> GetNews()
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                IList<News> news = newsRepository.FindAll();
                var _news = news.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                return new FindAllItemReponse<NewsModel>
                {
                    Items = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<NewsModel> GetNews(int pageSize, int pageIndex)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                var result = newsRepository.FindAll(pageSize, pageIndex);
                var _news = result.Item2.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                return new FindAllItemReponse<NewsModel>
                {
                    Count = result.Item1,
                    Items = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<NewsModel> FindNewsByID(string id)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                News news = newsRepository.FindByID(id);
                var _news = Mapper.Map<News, NewsModel>(news);
                return new FindItemReponse<NewsModel>
                {
                    Item = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public BaseResponse DeleteNews(string id)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                newsRepository.Delete(id);
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


        public BaseResponse UpdateNews(NewsModel news)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                News _news = Mapper.Map<NewsModel, News>(news);
                newsRepository.Update(_news);
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

        public InsertResponse CreateNews(NewsModel news)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                News _news = Mapper.Map<NewsModel, News>(news);
                object id = newsRepository.Insert(_news);
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

        public FindItemReponse<NewsModel> FindNewsByActionURL(string actionURL)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                News news = newsRepository.FindByActionURL(actionURL);
                var _news = Mapper.Map<News, NewsModel>(news);
                return new FindItemReponse<NewsModel>
                {
                    Item = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<NewsModel> GetNewsByID(string newsID)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                News news = newsRepository.FindByID(newsID);
                var _news = Mapper.Map<News, NewsModel>(news);
                return new FindItemReponse<NewsModel>
                {
                    Item = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<NewsModel> GetNews(int pageSize, int pageIndex, string language)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                var result = newsRepository.FindAll(pageSize, pageIndex, language);
                var _news = result.Item2.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                return new FindAllItemReponse<NewsModel>
                {
                    Count = result.Item1,
                    Items = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindAllItemReponse<NewsModel> GetTopNews(int top, string language)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                IList<News> news = newsRepository.FindTop(top, language);
                var _news = news.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                return new FindAllItemReponse<NewsModel>
                {
                    Items = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };

            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<NewsModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
