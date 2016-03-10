using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class VideoRepository : IVideoRepository
    {
        public Video FindByActionURL(string actionURL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Videos.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
            }
        }

        public IList<Video> FindTop(int top, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Videos.Where(n => n.Language.Equals(language)).OrderByDescending(n => n.CreatedDate).Take(top).ToList();
            }
        }

        public Tuple<int, IList<Video>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.Videos.Count();
                var items = context.Videos.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<Video>>(count, items);
            }
        }

        public Tuple<int, IList<Video>> FindAll(int pageSize, int pageIndex, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var items = context.Videos.Where(n => n.Language.Equals(language)).OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Videos.Where(n => n.Language.Equals(language)).Count();
                return Tuple.Create<int, IList<Video>>(count, items);
            }
        }

        public Tuple<int, IList<Video>> FindAllRelated(DateTime date, int pageSize, int pageIndex, string language)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var items = context.Videos.OrderByDescending(n => n.CreatedDate).Where(n => n.Language.Equals(language) && n.CreatedDate < date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Videos.OrderByDescending(n => n.CreatedDate).Where(n => n.Language.Equals(language) && n.CreatedDate < date).Count();
                return Tuple.Create<int, IList<Video>>(count, items);
            }
        }

        public IList<Video> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Videos.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "Video")).ToList();
            }
        }

        public object Insert(Video item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Videos.Add(item);
                context.SaveChanges();
                return item.VideoID;
            }
        }

        public void Update(Video item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var result = context.Videos.Where(n => n.VideoID.Equals(item.VideoID)).SingleOrDefault();
                if (result != null)
                {
                    result.Title = item.Title;
                    if (!string.IsNullOrEmpty(item.ActionURL))
                    {
                        result.ActionURL = item.ActionURL;
                    }

                    if (!string.IsNullOrEmpty(item.VideoURL))
                    {
                        result.VideoURL = item.VideoURL;
                    }
                    result.Contents = item.Contents;
                    result.ShortContent = item.ShortContent;
                    result.UpdatedBy = item.UpdatedBy;
                    result.UpdatedDate = DateTime.Now;
                    result.Language = item.Language;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Video's id {0} invalid", item.VideoID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var item = context.Videos.Where(n => n.VideoID.Equals(_id)).SingleOrDefault();
                if (item != null)
                {
                    context.Videos.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Video's id {0} invalid", item.VideoID));
                }
            }
        }

        public Video FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Videos.Where(a => a.VideoID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Video> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Videos.OrderByDescending(n => n.CreatedDate).ToList();
            }
        }
    }
}
