using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class YouthScholarshipRepository : IYouthScholarshipRepository
    {
        public object Insert(YouthScholarship item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.YouthScholarships.Add(item);
                context.SaveChanges();
                return item.YouthScholarshipID;
            }
        }

        public void Update(YouthScholarship item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var scholarship = context.YouthScholarships.Where(a => a.YouthScholarshipID.Equals(item.YouthScholarshipID)).SingleOrDefault();
                if (scholarship != null)
                {
                    scholarship.MotivationIssue = item.MotivationIssue;
                    scholarship.MotivationReason = item.MotivationReason;
                    scholarship.MotivationResolving = item.MotivationResolving;
                    scholarship.PlanMaking = item.PlanMaking;
                    scholarship.UpdatedBy = item.UpdatedBy;
                    scholarship.UpdatedDate = DateTime.Now;
                    scholarship.UploadFile = !string.IsNullOrEmpty(item.UploadFile) ? item.UploadFile : scholarship.UploadFile;
                    scholarship.UserID = item.UserID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Youth scholarship id {0} invalid", item.YouthScholarshipID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var scholarship = context.YouthScholarships.Where(a => a.YouthScholarshipID.Equals(_id)).SingleOrDefault();
                if (scholarship != null)
                {
                    context.YouthScholarships.Remove(scholarship);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Youth scholarship id {0} invalid", id));
                }
            }
        }

        public YouthScholarship FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.YouthScholarships.Where(a => a.YouthScholarshipID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<YouthScholarship> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.YouthScholarships.ToList();
            }
        }

        public YouthScholarship FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.YouthScholarships.Where(a => a.UserID.Equals(userID)).SingleOrDefault();
            }
        }
    }
}
