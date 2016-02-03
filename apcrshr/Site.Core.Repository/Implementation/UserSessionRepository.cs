using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class UserSessionRepository : IUserSessionRepository
    {
        public object Insert(UserSession item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.UserSessions.Add(item);
                context.SaveChanges();
                return item.SessionID;
            }
        }

        public void Update(UserSession item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var usersession = context.UserSessions.Where(a => a.SessionID.Equals(item.SessionID)).SingleOrDefault();
                if (usersession != null)
                {
                    usersession.UpdatedDate = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("UserSession id {0} invalid", item.SessionID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var usersession = context.UserSessions.Where(a => a.SessionID.Equals(_id)).SingleOrDefault();
                if (usersession != null)
                {
                    context.UserSessions.Remove(usersession);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("UserSession id {0} invalid", id));
                }
            }
        }

        public UserSession FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.UserSessions.Where(a => a.SessionID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<UserSession> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.UserSessions.ToList();
            }
        }

        public void DeleteByUserID(object userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = userID.ToString();
                var userSession = context.UserSessions.Where(a => a.UserID.Equals(_id)).SingleOrDefault();
                if (userSession != null)
                {
                    context.UserSessions.Remove(userSession);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("User id {0} invalid", _id));
                }
            }
        }

        public UserSession FindByUserID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.UserSessions.Where(a => a.UserID.Equals(_id)).SingleOrDefault();
            }
        }
    }
}
