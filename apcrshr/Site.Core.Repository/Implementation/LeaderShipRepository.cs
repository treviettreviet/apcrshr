using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class LeaderShipRepository : ILeaderShipRepository
    {
        public object Insert(LeaderShip item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.LeaderShips.Add(item);
                context.SaveChanges();
                return item.LeaderShipID;
            }
        }

        public void Update(LeaderShip item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var leader = context.LeaderShips.Where(a => a.LeaderShipID.Equals(item.LeaderShipID)).SingleOrDefault();
                if (leader != null)
                {
                    leader.Duties = item.Duties;
                    leader.EndDate = item.EndDate;
                    leader.FromDate = item.FromDate;
                    leader.LeaderNow = item.LeaderNow;
                    leader.Organization = item.Organization;
                    leader.Position = item.Position;
                    leader.YouthScholarshipID = item.YouthScholarshipID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("LeaderShip id {0} invalid", item.LeaderShipID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var leader = context.LeaderShips.Where(a => a.LeaderShipID.Equals(_id)).SingleOrDefault();
                if (leader != null)
                {
                    context.LeaderShips.Remove(leader);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("LeaderShip id {0} invalid", id));
                }
            }
        }

        public LeaderShip FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.LeaderShips.Where(a => a.LeaderShipID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<LeaderShip> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.LeaderShips.ToList();
            }
        }
    }
}
