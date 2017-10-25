using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class LogisticSheduleRepository : ILogisticSheduleRepository
    {
        public object Insert(LogisticSchedule item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.LogisticSchedules.Add(item);
                context.SaveChanges();
                return item.LogisticID;
            }
        }

        public void Update(LogisticSchedule item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var logistic = context.LogisticSchedules.Where(a => a.LogisticID.Equals(item.LogisticID)).SingleOrDefault();
                if (logistic != null)
                {
                    logistic.ArrivalDate = item.ArrivalDate;
                    logistic.ArrivalFlightNumber = item.ArrivalFlightNumber;
                    logistic.ArrivalGate = item.ArrivalGate;
                    logistic.DepartureDate = item.DepartureDate;
                    logistic.DepartureFlightNumber = item.DepartureFlightNumber;
                    logistic.DepartureGate = item.DepartureGate;
                    logistic.SpecialRequirement = item.SpecialRequirement;
                    logistic.WhenNeedPick = item.WhenNeedPick;
                    logistic.HotelName = item.HotelName;
                    logistic.CheckinDate = item.CheckinDate;
                    logistic.CheckoutDate = item.CheckoutDate;
                    logistic.ConferenceRoles = item.ConferenceRoles;
                    logistic.AirportService = item.AirportService;
                    logistic.VisaProcess = item.VisaProcess;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Logistic id {0} invalid", item.LogisticID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var logistic = context.LogisticSchedules.Where(a => a.LogisticID.Equals(_id)).SingleOrDefault();
                if (logistic != null)
                {
                    context.LogisticSchedules.Remove(logistic);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Logistic id {0} invalid", id));
                }
            }
        }

        public LogisticSchedule FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.LogisticSchedules.Where(a => a.LogisticID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<LogisticSchedule> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.LogisticSchedules.ToList();
            }
        }

        public LogisticSchedule FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.LogisticSchedules.Where(a => a.UserID.Equals(userID)).FirstOrDefault();
            }
        }
    }
}
