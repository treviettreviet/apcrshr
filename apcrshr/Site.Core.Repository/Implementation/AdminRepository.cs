using Site.Core.Common.Ultil.Enum;
using Site.Core.Common.Ultil.Security;
using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        public object Insert(Admin item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                item.Password = Encryption.ComputeHash(item.Password, Algorithm.SHA384, null);
                context.Admins.Add(item);
                context.SaveChanges();
                return item.AdminID;
            }
        }

        public void Update(Admin item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var admin = context.Admins.Where(a => a.AdminID.Equals(item.AdminID)).SingleOrDefault();
                if (admin != null)
                {
                    admin.BirthDate = item.BirthDate;
                    admin.Email = item.Email;
                    admin.FirstName = item.FirstName;
                    admin.LastName = item.LastName;
                    admin.Locked = item.Locked;
                    admin.Password = string.IsNullOrEmpty(item.Password) ? admin.Password : Encryption.ComputeHash(item.Password, Algorithm.SHA384, null);
                    admin.Phone = item.Phone;
                    admin.UserName = item.UserName;
                    admin.Type = item.Type;
                    admin.Roles = item.Roles;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Admin id {0} invalid", item.AdminID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var admin = context.Admins.Where(a => a.AdminID.Equals(_id)).SingleOrDefault();
                if (admin != null)
                {
                    context.Admins.Remove(admin);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Admin id {0} invalid", id));
                }
            }
        }

        public Admin FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Admins.Where(a => a.AdminID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Admin> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Admins.ToList();
            }
        }

        public Admin Login(string username, string password)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                Admin admin = context.Admins.Where(a => a.UserName == username).SingleOrDefault();
                if (admin != null && Encryption.VerifyHash(password, Algorithm.SHA384, admin.Password) && admin.Locked == false)
                {
                    return admin;
                }
                return null;
            }
        }

        public IList<Admin> GetAdminsExceptMe(string adminID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Admins.Where(a => !a.AdminID.Equals(adminID) && !a.UserName.Equals("administrator", StringComparison.OrdinalIgnoreCase) && a.Type != 1).ToList();
            }
        }

        public Admin FindByUserName(string username)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Admins.Where(a => a.UserName.Equals(username)).SingleOrDefault();
            }
        }


        public IList<Admin> FindStandardAdmins()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Admins.Where(a => a.Type != 1).ToList();
            }
        }
    }
}
