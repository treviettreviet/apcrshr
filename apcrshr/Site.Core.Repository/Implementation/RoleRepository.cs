using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        public object Insert(Role item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Roles.Add(item);
                context.SaveChanges();
                return item.RoleID;
            }
        }

        public void Update(Role item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var role = context.Roles.Where(a => a.RoleID.Equals(item.RoleID)).SingleOrDefault();
                if (role != null)
                {
                    role.Description = item.Description;
                    role.Name = item.Name;
                    role.Type = item.Type;
                    role.UpdatedBy = item.UpdatedBy;
                    role.UpdatedDate = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Role id {0} invalid", item.RoleID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var role = context.Roles.Where(a => a.RoleID.Equals(_id)).SingleOrDefault();
                if (role != null)
                {
                    context.Roles.Remove(role);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Role id {0} invalid", id));
                }
            }
        }

        public Role FindByID(object id)
        {
            APCRSHREntities context = new APCRSHREntities();
            var _id = id.ToString();
            return context.Roles.Where(a => a.RoleID.Equals(_id)).SingleOrDefault();
        }

        public IList<Role> FindAll()
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Roles.ToList();
        }
    }
}
