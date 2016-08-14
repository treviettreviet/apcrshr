using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class MainScholarshipRepository : IMainScholarshipRepository
    {
        public object Insert(MainScholarship item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.MainScholarships.Add(item);
                context.SaveChanges();
                return item.ScholarshipID;
            }
        }

        public void Update(MainScholarship item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var scholarship = context.MainScholarships.Where(a => a.ScholarshipID.Equals(item.ScholarshipID)).SingleOrDefault();
                if (scholarship != null)
                {
                    scholarship.HasSubmitted = item.HasSubmitted;
                    scholarship.Organization = item.Organization;
                    scholarship.Position = item.Position;
                    scholarship.ReasonScholarship = item.ReasonScholarship;
                    scholarship.Responsibility = item.Responsibility;
                    scholarship.UpdatedBy = item.UpdatedBy;
                    scholarship.UpdatedDate = DateTime.Now;
                    scholarship.DurationOfWork = item.DurationOfWork;
                    scholarship.SubmissionTitles = item.SubmissionTitles;
                    scholarship.UserID = item.UserID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Scholarship id {0} invalid", item.ScholarshipID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var scholarship = context.MainScholarships.Where(a => a.ScholarshipID.Equals(_id)).SingleOrDefault();
                if (scholarship != null)
                {
                    context.MainScholarships.Remove(scholarship);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Scholarship id {0} invalid", id));
                }
            }
        }

        public MainScholarship FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.MainScholarships.Where(a => a.ScholarshipID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<MainScholarship> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.MainScholarships.ToList();
            }
        }

        public MainScholarship FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.MainScholarships.Where(m => m.UserID.Equals(userID)).SingleOrDefault();
            }
        }
    }
}
