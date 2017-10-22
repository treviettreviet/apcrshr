using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        public IList<TransactionHistory> FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.TransactionHistories.Where(t => t.UserId == userID).ToList();
            }
        }

        public object Insert(TransactionHistory item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.TransactionHistories.Add(item);
                context.SaveChanges();
                return item.Id;
            }
        }

        public void Update(TransactionHistory item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var transaction = context.TransactionHistories.Where(u => u.Id.Equals(item.Id)).SingleOrDefault();
                if (transaction != null)
                {
                    transaction.Action = item.Action;
                    transaction.Log = item.Log;
                    transaction.Status = item.Status;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Transaction id {0} invalid", item.Id));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var transaction = context.TransactionHistories.Where(a => a.Id.Equals(_id)).SingleOrDefault();
                if (transaction != null)
                {
                    context.TransactionHistories.Remove(transaction);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Transaction id {0} invalid", id));
                }
            }
        }

        public TransactionHistory FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.TransactionHistories.Where(a => a.Id.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<TransactionHistory> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.TransactionHistories.OrderByDescending(u => u.CreatedDate).ToList();
            }
        }
    }
}
