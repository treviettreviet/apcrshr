using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class ResourceRepository : IResourceRepository
    {
        public object Insert(Resource item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Resources.Add(item);
                context.SaveChanges();
                return item.ResourceID;
            }
        }

        public void Update(Resource item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var resource = context.Resources.Where(a => a.ResourceID.Equals(item.ResourceID)).SingleOrDefault();
                if (resource != null)
                {
                    resource.Title = item.Title;
                    resource.UpdatedBy = item.UpdatedBy;
                    resource.UpdatedDate = DateTime.Now;
                    resource.URL = item.URL;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Resource id {0} invalid", item.ResourceID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var resource = context.Resources.Where(a => a.ResourceID.Equals(_id)).SingleOrDefault();
                if (resource != null)
                {
                    context.Resources.Remove(resource);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Resource id {0} invalid", id));
                }
            }
        }

        public Resource FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Resources.Where(a => a.ResourceID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Resource> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Resources.ToList();
            }
        }
    }
}
