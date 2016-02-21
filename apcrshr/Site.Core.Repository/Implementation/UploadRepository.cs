using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class UploadRepository : IUploadRepository
    {
        public object Insert(Upload item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Uploads.Add(item);
                context.SaveChanges();
                return item.UploadID;
            }
        }

        public void Update(Upload item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var upload = context.Uploads.Where(u => u.UploadID.Equals(item.UploadID)).SingleOrDefault();
                if (upload != null)
                {
                    upload.Description = item.Description;
                    upload.Title = item.Title;
                    upload.UpdatedBy = item.UpdatedBy;
                    upload.UpdatedDate = DateTime.Now;
                    upload.UploadURL = item.UploadURL;
                    upload.FilePath = item.FilePath;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Upload id {0} invalid", item.UploadID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var upload = context.Uploads.Where(a => a.UploadID.Equals(_id)).SingleOrDefault();
                if (upload != null)
                {
                    context.Uploads.Remove(upload);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Upload id {0} invalid", id));
                }
            }
        }

        public Upload FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Uploads.Where(a => a.UploadID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Upload> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Uploads.OrderByDescending(u => u.CreatedDate).ToList();
            }
        }

        public IList<Upload> FindAll(int top)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Uploads.OrderByDescending(u => u.CreatedDate).Take(top).ToList();
            }
        }
    }
}
