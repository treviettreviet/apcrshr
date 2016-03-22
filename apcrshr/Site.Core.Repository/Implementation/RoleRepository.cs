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
        private static readonly string ADMINISTRATOR_ROLE_ID = "2ea7fd43-7704-430f-add6-4082c8a95d8d";
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
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Roles.Where(a => a.RoleID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Role> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Roles.ToList();
            }
        }

        public IList<Role> FindAllAvailables(string adminID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Roles.SqlQuery("SELECT * FROM [Role] WHERE [RoleID] NOT IN (SELECT [RoleID] FROM [AdminRole] WHERE [AdminID] = @p0) AND [RoleID] != @p1", adminID, ADMINISTRATOR_ROLE_ID).ToList();
            }
        }


        public IList<Role> FindAllAssignedRoles(string adminID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Roles.SqlQuery("SELECT * FROM [Role] WHERE [RoleID] IN (SELECT [RoleID] FROM [AdminRole] WHERE [AdminID] = @p0) AND [RoleID] != @p1", adminID, ADMINISTRATOR_ROLE_ID).ToList();
            }
        }
    }
}
