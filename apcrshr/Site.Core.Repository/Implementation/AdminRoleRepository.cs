using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class AdminRoleRepository : IAdminRoleRepository
    {
        public object Insert(AdminRole item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.AdminRoles.Add(item);
                context.SaveChanges();
                return item.AdminID;
            }
        }

        public void Update(AdminRole item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public AdminRole FindByID(object id)
        {
            throw new NotImplementedException();
        }

        public IList<AdminRole> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.AdminRoles.ToList();
            }
        }

        public AdminRole FindByID(string adminID, string roleID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.AdminRoles.Where(a => a.AdminID.Equals(adminID) && a.RoleID.Equals(roleID)).SingleOrDefault();
            }
        }

        public void Delete(string adminID, string roleID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var item = context.AdminRoles.Where(a => a.AdminID.Equals(adminID) && a.RoleID.Equals(roleID)).SingleOrDefault();
                if (item != null)
                {
                    context.AdminRoles.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Admin id {0} role id {1} invalid", adminID, roleID));
                }
            }
        }
    }
}
