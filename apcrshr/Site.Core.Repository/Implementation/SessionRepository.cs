using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class SessionRepository : ISessionRepository
    {
        public object Insert(Session item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Sessions.Add(item);
                context.SaveChanges();
                return item.SessionID;
            }
        }

        public void Update(Session item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var session = context.Sessions.Where(a => a.SessionID.Equals(item.SessionID)).SingleOrDefault();
                if (session != null)
                {
                    session.Options = item.Options;
                    session.UpdatedDate = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Session id {0} invalid", item.SessionID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var session = context.Sessions.Where(a => a.SessionID.Equals(_id)).SingleOrDefault();
                if (session != null)
                {
                    context.Sessions.Remove(session);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Session id {0} invalid", id));
                }
            }
        }

        public Session FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Sessions.Where(a => a.SessionID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Session> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Sessions.ToList();
            }
        }
    }
}
