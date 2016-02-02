using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class NewsRepository : INewsRepository
    {
        public object Insert(News item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.News.Add(item);
                context.SaveChanges();
                return item.NewsID;
            }
        }

        public void Update(News item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var news = context.News.Where(a => a.NewsID.Equals(item.NewsID)).SingleOrDefault();
                if (news != null)
                {
                    news.ActionURL = item.ActionURL;
                    news.Contents = item.Contents;
                    news.Language = item.Language;
                    news.Title = item.Title;
                    news.UpdatedBy = item.UpdatedBy;
                    news.UpdatedDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(item.ThumbnailURL))
                    {
                        news.ThumbnailURL = item.ThumbnailURL;
                    }
                    news.MenuID = item.MenuID;
                    news.ShortContent = item.ShortContent;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("News id {0} invalid", item.NewsID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var news = context.News.Where(a => a.NewsID.Equals(_id)).SingleOrDefault();
                if (news != null)
                {
                    context.News.Remove(news);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("News id {0} invalid", id));
                }
            }
        }

        public News FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.News.Where(a => a.NewsID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<News> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.News.OrderByDescending(n => n.CreatedDate).ToList();
            }
        }

        public Tuple<int, IList<News>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.News.Count();
                var news = context.News.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<News>>(count, news);
            }
        }

        public IList<News> FindTop(int top, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.News.Where(n => n.Language.Equals(language)).OrderByDescending(n => n.CreatedDate).Take(top).ToList();
            }
        }

        public News FindByActionURL(string actionURL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.News.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
            }
        }


        public Tuple<int, IList<News>> FindAll(int pageSize, int pageIndex, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var news = context.News.Where(n => n.Language.Equals(language)).OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.News.Where(n => n.Language.Equals(language)).Count();
                return Tuple.Create<int, IList<News>>(count, news);
            }
        }


        public Tuple<int, IList<News>> FindAll(int pageSize, int pageIndex, string language, string menuID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var news = context.News.Where(n => n.Language.Equals(language) && n.MenuID.Equals(menuID)).OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.News.Where(n => n.Language.Equals(language) && n.MenuID.Equals(menuID)).Count();
                return Tuple.Create<int, IList<News>>(count, news);
            }
        }

        public Tuple<int, IList<News>> FindAllRelated(string menuID, DateTime date, int pageSize, int pageIndex, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var news = context.News.OrderByDescending(n => n.CreatedDate).Where(n => n.Language.Equals(language) && n.MenuID.Equals(menuID) && n.CreatedDate < date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.News.OrderByDescending(n => n.CreatedDate).Where(n => n.Language.Equals(language) && n.MenuID.Equals(menuID) && n.CreatedDate < date).Count();
                return Tuple.Create<int, IList<News>>(count, news);
            }
        }


        public IList<News> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.News.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "News")).ToList();
            }
        }
    }
}
