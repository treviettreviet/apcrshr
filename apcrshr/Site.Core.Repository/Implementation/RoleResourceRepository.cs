using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class RoleResourceRepository : IRoleResourceRepository
    {
        public object Insert(RoleResource item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.RoleResources.Add(item);
                context.SaveChanges();
                return item.RoleID;
            }
        }

        public void Update(RoleResource item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public RoleResource FindByID(object id)
        {
            throw new NotImplementedException();
        }

        public IList<RoleResource> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.RoleResources.ToList();
            }
        }

        public RoleResource FindByID(string resourceID, string roleID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.RoleResources.Where(a => a.ResourceID.Equals(resourceID) && a.RoleID.Equals(roleID)).SingleOrDefault();
            }
        }

        public void Delete(string resourceID, string roleID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var item = context.RoleResources.Where(a => a.ResourceID.Equals(resourceID) && a.RoleID.Equals(roleID)).SingleOrDefault();
                if (item != null)
                {
                    context.RoleResources.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Resource id {0} role id {1} invalid", resourceID, roleID));
                }
            }
        }
    }
}
