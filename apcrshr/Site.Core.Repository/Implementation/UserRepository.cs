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
    public class UserRepository : IUserRepository
    {
        public object Insert(User item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                item.Password = Encryption.ComputeHash(item.Password, Algorithm.SHA384, null);
                context.Users.Add(item);
                context.SaveChanges();
                return item.UserID;
            }
        }

        public void Update(User item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var user = context.Users.Where(u => u.UserID.Equals(item.UserID)).SingleOrDefault();
                if (user != null)
                {
                    user.Email = item.Email;
                    user.DateOfBirth = item.DateOfBirth;
                    user.FullName = item.FullName;
                    user.Locked = item.Locked;
                    user.OtherEmail = item.OtherEmail;
                    user.Password = string.IsNullOrEmpty(item.Password) ? user.Password : Encryption.ComputeHash(item.Password, Algorithm.SHA384, null);
                    user.UserName = item.UserName;
                    user.UpdatedDate = DateTime.Now;
                    user.Sex = item.Sex;
                    user.PhoneNumber = item.PhoneNumber;
                    user.Title = item.Title;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("User id {0} invalid", item.UserID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var user = context.Users.Where(a => a.UserID.Equals(_id)).SingleOrDefault();
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("User id {0} invalid", id));
                }
            }
        }

        public User FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Users.Where(a => a.UserID.Equals(_id)).SingleOrDefault();
            }
        }

        public User FindByEmail(string email)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _email = email.ToString();
                return context.Users.Where(u => u.Email == _email).SingleOrDefault();
            }
        }

        public User FindByUserName(string username)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Users.Where(u => u.UserName.Equals(username)).SingleOrDefault();
            }
        }

        public IList<User> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Users.ToList();
            }
        }

        public User Login(string username, string password)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                User user = context.Users.Where(u => u.UserName.Equals(username)).SingleOrDefault();
                if (user != null && Encryption.VerifyHash(password, Algorithm.SHA384, user.Password))
                {
                    return user;
                }
                return null;
            }
        }

        public IList<User> GetUserExceptMe(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Users.Where(u => !u.UserID.Equals(userID)).ToList();
            }
        }
    }
}
