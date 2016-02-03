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
        public GetAllNewsResponse GetNews()
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    IList<News> news = newsRepository.FindAll();
                    var _news = news.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                    return new GetAllNewsResponse
                    {
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllNewsResponse GetNews(int pageSize, int pageIndex)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    var result = newsRepository.FindAll(pageSize, pageIndex);
                    var _news = result.Item2.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                    return new GetAllNewsResponse
                    {
                        Count = result.Item1,
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetNewsResponse FindNewsByID(string id)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    News news = newsRepository.FindByID(id);
                    var _news = Mapper.Map<News, NewsModel>(news);
                    return new GetNewsResponse
                    {
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public BaseResponse DeleteNews(string id)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    newsRepository.Delete(id);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_delete_success
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
        }


        public BaseResponse UpdateNews(NewsModel news)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    News _news = Mapper.Map<NewsModel, News>(news);
                    newsRepository.Update(_news);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_update_success
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
        }

        public GetAllNewsCategoryResponse GetNewsCategories()
        {
            using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
            {
                try
                {
                    IList<NewsCategory> newsCate = newsCateRepository.FindAll();
                    var _newsCate = newsCate.Select(n => Mapper.Map<NewsCategory, NewsCategoryModel>(n)).ToList();
                    return new GetAllNewsCategoryResponse
                    {
                        NewsCategories = _newsCate,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllNewsCategoryResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public CreateNewsCategoryResponse CreateNewsCategory(NewsCategoryModel newsCategory)
        {
            using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
            {
                try
                {
                    NewsCategory category = Mapper.Map<NewsCategoryModel, NewsCategory>(newsCategory);
                    object id = newsCateRepository.Insert(category);
                    return new CreateNewsCategoryResponse
                    {
                        CategoryID = id.ToString(),
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_create_success
                    };
                }
                catch (Exception ex)
                {
                    return new CreateNewsCategoryResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public CreateNewsResponse CreateNews(NewsModel news)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    News _news = Mapper.Map<NewsModel, News>(news);
                    object id = newsRepository.Insert(_news);
                    return new CreateNewsResponse
                    {
                        NewsID = id.ToString(),
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_create_success
                    };
                }
                catch (Exception ex)
                {
                    return new CreateNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetNewsResponse FindNewsByActionURL(string actionURL)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    News news = newsRepository.FindByActionURL(actionURL);
                    var _news = Mapper.Map<News, NewsModel>(news);
                    return new GetNewsResponse
                    {
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllNewsResponse GetNewsByCategory(string categoryActionURL, int pageSize, int pageIndex, string language)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    INewsCategoryRepository newsCategoryRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository();

                    var category = newsCategoryRepository.FindByActionURL(categoryActionURL);
                    var result = newsRepository.FindAll(pageSize, pageIndex, language, category.CategoryID);
                    var _news = result.Item2.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                    return new GetAllNewsResponse
                    {
                        Count = result.Item1,
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public BaseResponse DeleteNewsCategory(string categoryID)
        {
            using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
            {
                try
                {
                    newsCateRepository.Delete(categoryID);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
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
        }

        public GetNewsCategoryResponse GetNewsCategoryByID(string categoryID)
        {
            using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
            {
                try
                {
                    NewsCategory category = newsCateRepository.FindByID(categoryID);
                    NewsCategoryModel _category = Mapper.Map<NewsCategory, NewsCategoryModel>(category);
                    return new GetNewsCategoryResponse
                    {
                        NewsCategory = _category,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetNewsCategoryResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public BaseResponse UpdateNewsCategory(NewsCategoryModel newsCategory)
        {
            using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
            {
                try
                {
                    NewsCategory category = Mapper.Map<NewsCategoryModel, NewsCategory>(newsCategory);
                    newsCateRepository.Update(category);
                    return new GetNewsCategoryResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_update_success
                    };
                }
                catch (Exception ex)
                {
                    return new GetNewsCategoryResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetNewsResponse GetNewsByID(string newsID)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    News news = newsRepository.FindByID(newsID);
                    var _news = Mapper.Map<News, NewsModel>(news);
                    return new GetNewsResponse
                    {
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllNewsCategoryResponse GetNewsCategories(string language)
        {
            using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
            {
                try
                {
                    IList<NewsCategory> newsCate = newsCateRepository.FindAll(language);
                    var _newsCate = newsCate.Select(n => Mapper.Map<NewsCategory, NewsCategoryModel>(n)).ToList();
                    return new GetAllNewsCategoryResponse
                    {
                        NewsCategories = _newsCate,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllNewsCategoryResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllNewsResponse GetNews(int pageSize, int pageIndex, string language)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    var result = newsRepository.FindAll(pageSize, pageIndex, language);
                    var _news = result.Item2.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                    return new GetAllNewsResponse
                    {
                        Count = result.Item1,
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllNewsResponse GetTopNews(int top, string language)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                try
                {
                    IList<News> news = newsRepository.FindTop(top, language);
                    var _news = news.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                    return new GetAllNewsResponse
                    {
                        News = _news,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };

                }
                catch (Exception ex)
                {
                    return new GetAllNewsResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetAllNewsResponse GetRelatedNews(string category, DateTime date, int pageSize, int pageIndex, string language)
        {
            using (INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository())
            {
                using (INewsCategoryRepository newsCateRepository = RepositoryClassFactory.GetInstance().GetNewsCategoryRepository())
                {
                    try
                    {
                        var result = newsRepository.FindAllRelated(category, date, pageSize, pageIndex, language);
                        var _news = result.Item2.Select(n => Mapper.Map<News, NewsModel>(n)).ToList();
                        return new GetAllNewsResponse
                        {
                            Count = result.Item1,
                            News = _news,
                            ErrorCode = (int)ErrorCode.None,
                            Message = string.Empty
                        };
                    }
                    catch (Exception ex)
                    {
                        return new GetAllNewsResponse
                        {
                            ErrorCode = (int)ErrorCode.Error,
                            Message = ex.Message
                        };
                    }
                }
            }
        }
    }
}
