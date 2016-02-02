using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class SubscriberRepository : ISubscriberRepository
    {
        public object Insert(Subscriber item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Subscribers.Add(item);
                context.SaveChanges();
                return item.SubscriberID;
            }
        }

        public void Update(Subscriber item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var sub = context.Subscribers.Where(s => s.SubscriberID.Equals(item.SubscriberID)).SingleOrDefault();
                if (sub != null)
                {
                    sub.FirstName = item.FirstName;
                    sub.LastName = item.LastName;
                    sub.Email = item.Email;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Subscriber id {0} is invalid", item.SubscriberID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var sub = context.Subscribers.Where(s => s.SubscriberID.Equals(_id)).SingleOrDefault();
                if (sub != null)
                {
                    context.Subscribers.Remove(sub);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Subscriber id {0} is invalid", id));
                }
            }
        }

        public Subscriber FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Subscribers.Where(s => s.SubscriberID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Subscriber> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Subscribers.OrderByDescending(s => s.CreatedDate).ToList();
            }
        }

        public Subscriber FindByEmail(string email)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Subscribers.Where(s => s.Email.Equals(email)).FirstOrDefault();
            }
        }
    }
}
