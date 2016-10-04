using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        public object Insert(Payment item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Payments.Add(item);
                context.SaveChanges();
                return item.PaymentID;
            }
        }

        public void Update(Payment item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var payment = context.Payments.Where(a => a.PaymentID.Equals(item.PaymentID)).SingleOrDefault();
                if (payment != null)
                {
                    payment.Amount = item.Amount;
                    payment.PaymentType = item.PaymentType;
                    payment.Status = item.Status;
                    payment.UpdatedBy = item.UpdatedBy;
                    payment.UpdatedDate = DateTime.Now;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Payment id {0} invalid", item.PaymentID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var payment = context.Payments.Where(a => a.PaymentID.Equals(_id)).SingleOrDefault();
                if (payment != null)
                {
                    context.Payments.Remove(payment);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Payment id {0} invalid", id));
                }
            }
        }

        public Payment FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Payments.Where(a => a.PaymentID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Payment> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Payments.ToList();
            }
        }

        public IList<Payment> FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Payments.Where(p => p.UserID.Equals(userID)).ToList();
            }
        }
    }
}
