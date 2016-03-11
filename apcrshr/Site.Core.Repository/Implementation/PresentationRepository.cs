using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class PresentationRepository : IPresentationRepository
    {
        public Presentation FindByActionURL(string actionURL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Presentations.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
            }
        }

        public Tuple<int, IList<Presentation>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.Presentations.Count();
                var presentations = context.Presentations.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<Presentation>>(count, presentations);
            }
        }

        public Tuple<int, IList<Presentation>> FindAllRelated(DateTime date, int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var presentations = context.Presentations.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Presentations.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date).Count();
                return Tuple.Create<int, IList<Presentation>>(count, presentations);
            }
        }

        public IList<Presentation> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Presentations.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "Presentation")).ToList();
            }
        }

        public object Insert(Presentation item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Presentations.Add(item);
                context.SaveChanges();
                return item.PresentationID;
            }
        }

        public void Update(Presentation item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var result = context.Presentations.Where(n => n.PresentationID.Equals(item.PresentationID)).SingleOrDefault();
                if (result != null)
                {
                    result.Title = item.Title;
                    if (!string.IsNullOrEmpty(item.ActionURL))
                    {
                        result.ActionURL = item.ActionURL;
                    }

                    if (!string.IsNullOrEmpty(item.AttachmentURL))
                    {
                        result.AttachmentURL = item.AttachmentURL;
                    }
                    if (!string.IsNullOrEmpty(item.ImageURL))
                    {
                        result.ImageURL = item.ImageURL;
                    }
                    result.Contents = item.Contents;
                    result.ShortContent = item.ShortContent;
                    result.UpdatedBy = item.UpdatedBy;
                    result.UpdatedDate = DateTime.Now;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Presentation's id {0} invalid", item.PresentationID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var item = context.Presentations.Where(n => n.PresentationID.Equals(_id)).SingleOrDefault();
                if (item != null)
                {
                    context.Presentations.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Presentation's id {0} invalid", item.PresentationID));
                }
            }
        }

        public Presentation FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Presentations.Where(a => a.PresentationID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Presentation> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Presentations.OrderByDescending(n => n.CreatedDate).ToList();
            }
        }
    }
}
