using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class ImportantDeadlineRepository : IImportantDeadlineRepository
    {
        public object Insert(ImportantDeadline item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.ImportantDeadlines.Add(item);
                context.SaveChanges();
                return item.DeadlineID;
            }
        }

        public void Update(ImportantDeadline item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var importantDeadline = context.ImportantDeadlines.Where(i => i.DeadlineID.Equals(item.DeadlineID)).SingleOrDefault();
                if (importantDeadline != null)
                {
                    importantDeadline.Title = item.Title;
                    importantDeadline.ShortContent = item.ShortContent;
                    importantDeadline.Contents = item.Contents;
                    importantDeadline.ActionURL = !string.IsNullOrEmpty(item.ActionURL) ? item.ActionURL : importantDeadline.ActionURL;
                    importantDeadline.StartDate = item.StartDate;
                    importantDeadline.EndDate = item.EndDate;
                    importantDeadline.Deadline = item.Deadline;
                    importantDeadline.UpdatedDate = DateTime.Now;
                    importantDeadline.Type = item.Type;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Important Deadline id {0} invalid", importantDeadline.DeadlineID));

                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var importantDeadline = context.ImportantDeadlines.Where(i => i.DeadlineID.Equals(_id)).SingleOrDefault();
                if (importantDeadline != null)
                {
                    context.ImportantDeadlines.Remove(importantDeadline);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Important Deadline id {0} invalid", id));
                }
            }
        }
        public ImportantDeadline FindByTitle(string title)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ImportantDeadlines.Where(i => i.Title.Equals(title)).SingleOrDefault(); 
            }
        }

        public ImportantDeadline FindByActionURL(string actionURL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ImportantDeadlines.Where(i => i.ActionURL.Equals(actionURL)).SingleOrDefault();
            }
        }

        public IList<ImportantDeadline> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ImportantDeadlines.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "ImportantDeadline")).ToList();
            }
        }
        public ImportantDeadline FindByID(object id)
        {
            var _id = id.ToString();
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ImportantDeadlines.Where(i => i.DeadlineID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<ImportantDeadline> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ImportantDeadlines.ToList();
            }
        }


        public IList<ImportantDeadline> FindTop(int top)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ImportantDeadlines.OrderByDescending(i => i.CreatedDate).Take(top).ToList();
            }
        }


        public Tuple<int, IList<ImportantDeadline>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.ImportantDeadlines.Count();
                var importantDeadline = context.ImportantDeadlines.OrderByDescending(i => i.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<ImportantDeadline>>(count, importantDeadline);
            }
        }


        public Tuple<int, IList<ImportantDeadline>> FindAllRelated(DateTime date, int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var deadlines = context.ImportantDeadlines.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.ImportantDeadlines.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date).Count();
                return Tuple.Create<int, IList<ImportantDeadline>>(count, deadlines);
            }
        }
    }
}
