using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class ConferenceDeclarationRepository : IConferenceDeclarationRepository
    {
       
        
        public Tuple<int, IList<ConferenceDeclaration>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.ConferenceDeclarations.Count();
                var conferenceDe = context.ConferenceDeclarations.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<ConferenceDeclaration>>(count, conferenceDe);
            }
        }

        public IList<ConferenceDeclaration> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ConferenceDeclarations.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "ConferenceDeclaration")).ToList();
            }
        }

        public object Insert(ConferenceDeclaration item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.ConferenceDeclarations.Add(item);
                context.SaveChanges();
                return item.ConferenceID;
            }
        }

        public void Update(ConferenceDeclaration item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var result = context.ConferenceDeclarations.Where(n => n.ConferenceID.Equals(item.ConferenceID)).SingleOrDefault();
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
                    throw new Exception(string.Format("Conference's id {0} invalid", item.ConferenceID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var item = context.ConferenceDeclarations.Where(n => n.ConferenceID.Equals(_id)).SingleOrDefault();
                if (item != null)
                {
                    context.ConferenceDeclarations.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Conference's id {0} invalid", item.ConferenceID));
                }
            }
        }

        public ConferenceDeclaration FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.ConferenceDeclarations.Where(a => a.ConferenceID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<ConferenceDeclaration> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.ConferenceDeclarations.OrderByDescending(n => n.CreatedDate).ToList();
            }
        }
    }
}
