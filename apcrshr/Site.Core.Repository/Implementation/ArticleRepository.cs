using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class ArticleRepository : IArticleRepository
    {
        public object Insert(Article item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Articles.Add(item);
                context.SaveChanges();
                return item.ArticleID;
            }
        }

        public void Update(Article item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var article = context.Articles.Where(a => a.ArticleID.Equals(item.ArticleID)).SingleOrDefault();
                if (article != null)
                {
                    article.ActionURL = item.ActionURL;
                    article.Contents = item.Contents;
                    article.Language = item.Language;
                    article.MenuID = item.MenuID;
                    article.ShortContent = item.ShortContent;
                    article.Title = item.Title;
                    article.UpdatedBy = item.UpdatedBy;
                    article.UpdatedDate = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Article id {0} invalid", item.ArticleID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var article = context.Articles.Where(a => a.ArticleID.Equals(_id)).SingleOrDefault();
                if (article != null)
                {
                    context.Articles.Remove(article);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Article id {0} invalid", id));
                }
            }
        }

        public Article FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Articles.Where(a => a.ArticleID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Article> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Articles.ToList();
            }
        }

        public Article FindByActionURL(string actionURL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Articles.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
            }
        }


        public Tuple<int, IList<Article>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.Articles.Count();
                var articles = context.Articles.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<Article>>(count, articles);
            }
        }

        public Tuple<int, IList<Article>> FindAll(int pageSize, int pageIndex, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var articles = context.Articles.Where(n => n.Language.Equals(language)).OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Articles.Where(n => n.Language.Equals(language)).Count();
                return Tuple.Create<int, IList<Article>>(count, articles);
            }
        }

        public Tuple<int, IList<Article>> FindAll(int pageSize, int pageIndex, string language, string menuID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var articles = context.Articles.Where(n => n.Language.Equals(language) && n.MenuID.Equals(menuID)).OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Articles.Where(n => n.Language.Equals(language) && n.MenuID.Equals(menuID)).Count();
                return Tuple.Create<int, IList<Article>>(count, articles);
            }
        }

        public IList<Article> FindByMenuID(string menuID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Articles.Where(a => a.MenuID.Equals(menuID)).ToList();
            }
        }


        public IList<Article> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Articles.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "Article")).ToList();
            }
        }
    }
}
