using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Service.Contract;
using Site.Core.DataModel.Response;
using Site.Core.Repository.Repository;
using Site.Core.Repository;
using Site.Core.DataModel.Model;
using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.Service.Implementation.ModelMapper;

namespace Site.Core.Service.Implementation
{
    public class HomeService : IHomeService
    {
        public static readonly string DEFAULT_LANGUAGE = "VN";

        public FindAllItemReponse<NewsModel> GetTopNews(int top, string language)
        {
            try
            {
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                IList<News> news = newsRepository.FindTop(top, language);
                var _news = news.Select(n => MapperUtil.CreateMapper().Mapper.Map<News, NewsModel>(n)).ToList();
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

        public SearchResultResponse Search(string keyword, int pageSize, int pageIndex)
        {
            SearchResultResponse response = new SearchResultResponse();
            List<SearchModel> list = new List<SearchModel>();

            try
            {
                //Search news
                INewsRepository newsRepository = RepositoryClassFactory.GetInstance().GetNewsRepository();
                IList<News> news = newsRepository.Search(keyword);
                foreach (var item in news)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "NewsDetail",
                        ActionURL = item.ActionURL,
                        Controller = "News",
                        CreatedDate = item.CreatedDate,
                        ImageURL = item.ThumbnailURL,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                //Search Conference
                IConferenceDeclarationRepository conRepository = RepositoryClassFactory.GetInstance().GetConferenceDeclarationRepository();
                IList<ConferenceDeclaration> cons = conRepository.Search(keyword);
                foreach (var item in cons)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "ConferenceDeclarationDetail",
                        ActionURL = item.ActionURL,
                        Controller = "ConferenceDeclaration",
                        CreatedDate = item.CreatedDate,
                        ImageURL = item.ImageURL,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                //Search Deadline
                IImportantDeadlineRepository deadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                IList<ImportantDeadline> deadlines = deadlineRepository.Search(keyword);
                foreach (var item in deadlines)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "ImportantDeadlineDetail",
                        ActionURL = item.ActionURL,
                        Controller = "ImportantDeadline",
                        CreatedDate = item.CreatedDate,
                        ImageURL = string.Empty,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                //Search Article
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                IList<Article> articles = articleRepository.Search(keyword);
                foreach (var item in articles)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "ArticleDetail",
                        ActionURL = item.ActionURL,
                        Controller = string.Empty,
                        CreatedDate = item.CreatedDate,
                        ImageURL = string.Empty,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                //Search Photo
                IPhotoRepository photoRepository = RepositoryClassFactory.GetInstance().GetPhotoRepository();
                IList<Photo> photos = photoRepository.Search(keyword);
                foreach (var item in deadlines)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "PhotoDetail",
                        ActionURL = item.ActionURL,
                        Controller = "Photo",
                        CreatedDate = item.CreatedDate,
                        ImageURL = string.Empty,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                //Search Album
                IAlbumRepository albumRepository = RepositoryClassFactory.GetInstance().GetAlbumRepository();
                IList<Album> albums = albumRepository.Search(keyword);
                foreach (var item in albums)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "AlbumDetail",
                        ActionURL = item.ActionURL,
                        Controller = "Album",
                        CreatedDate = item.CreatedDate,
                        ImageURL = string.Empty,
                        ShortContent = item.Title,
                        Title = item.Title
                    });
                }

                //Search Presentation
                IPresentationRepository presentationRepository = RepositoryClassFactory.GetInstance().GetPresentationRepository();
                IList<Presentation> pres = presentationRepository.Search(keyword);
                foreach (var item in pres)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "PresentationDetail",
                        ActionURL = item.ActionURL,
                        Controller = "Presentation",
                        CreatedDate = item.CreatedDate,
                        ImageURL = item.ImageURL,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                //Search Video
                IVideoRepository videoRepository = RepositoryClassFactory.GetInstance().GetVideoRepository();
                IList<Video> vids = videoRepository.Search(keyword);
                foreach (var item in vids)
                {
                    list.Add(new SearchModel()
                    {
                        Action = "VideoDetail",
                        ActionURL = item.ActionURL,
                        Controller = "Video",
                        CreatedDate = item.CreatedDate,
                        ImageURL = string.Empty,
                        ShortContent = item.ShortContent,
                        Title = item.Title
                    });
                }

                response.Count = list.Count;
                response.Result = list.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); ;

                if (list.Count == 0)
                {
                    return new SearchResultResponse()
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.text_searchNotFound, keyword.Substring(1, keyword.Length - 2))
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new SearchResultResponse()
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
