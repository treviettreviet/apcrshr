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
    public class ArticleService : IArticleService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.ArticleModel> FindArticleByID(string id)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                Article article = articleRepository.FindByID(id);
                var _articles = MapperUtil.CreateMapper().Mapper.Map<Article, ArticleModel>(article);
                return new FindItemReponse<ArticleModel>
                {
                    Item = _articles,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<ArticleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.ArticleModel> FindArticleByActionURL(string actionURL)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                Article article = articleRepository.FindByActionURL(actionURL);
                var _articles = MapperUtil.CreateMapper().Mapper.Map<Article, ArticleModel>(article);
                return new FindItemReponse<ArticleModel>
                {
                    Item = _articles,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<ArticleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeleteArticle(string id)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                articleRepository.Delete(id);
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

        public DataModel.Response.BaseResponse UpdateArticle(DataModel.Model.ArticleModel article)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                Article _article = MapperUtil.CreateMapper().Mapper.Map<ArticleModel, Article>(article);
                articleRepository.Update(_article);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ArticleModel> GetArticles()
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                IList<Article> articles = articleRepository.FindAll();
                var _articles = articles.Select(a => MapperUtil.CreateMapper().Mapper.Map<Article, ArticleModel>(a)).ToList();
                return new FindAllItemReponse<ArticleModel>
                {
                    Items = _articles,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ArticleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ArticleModel> GetArticles(int pageSize, int pageIndex)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                var result = articleRepository.FindAll(pageSize, pageIndex);
                var _news = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Article, ArticleModel>(n)).ToList();
                return new FindAllItemReponse<ArticleModel>
                {
                    Count = result.Item1,
                    Items = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ArticleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ArticleModel> GetArticles(int pageSize, int pageIndex, string language)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                var result = articleRepository.FindAll(pageSize, pageIndex, language);
                var _news = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Article, ArticleModel>(n)).ToList();
                return new FindAllItemReponse<ArticleModel>
                {
                    Count = result.Item1,
                    Items = _news,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ArticleModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse CreateArticle(DataModel.Model.ArticleModel article)
        {
            try
            {
                IArticleRepository articleRepository = RepositoryClassFactory.GetInstance().GetArticleRepository();
                Article _article = MapperUtil.CreateMapper().Mapper.Map<ArticleModel, Article>(article);
                object id = articleRepository.Insert(_article);
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
    }
}
