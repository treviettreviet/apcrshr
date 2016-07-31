using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class ExperienceRepository : IExperienceRepository
    {
        public object Insert(Experience item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Experiences.Add(item);
                context.SaveChanges();
                return item.WorkingID;
            }
        }

        public void Update(Experience item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var experience = context.Experiences.Where(a => a.WorkingID.Equals(item.WorkingID)).SingleOrDefault();
                if (experience != null)
                {
                    experience.Duties = item.Duties;
                    experience.Organization = item.Organization;
                    experience.Position = item.Position;
                    experience.WorkingEnd = item.WorkingEnd;
                    experience.WorkingNow = item.WorkingNow;
                    experience.WorkingStart = item.WorkingStart;
                    experience.YouthScholarshipID = item.YouthScholarshipID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Working id {0} invalid", item.WorkingID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var experience = context.Experiences.Where(a => a.WorkingID.Equals(_id)).SingleOrDefault();
                if (experience != null)
                {
                    context.Experiences.Remove(experience);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Working id {0} invalid", id));
                }
            }
        }

        public Experience FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Experiences.Where(a => a.WorkingID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Experience> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Experiences.ToList();
            }
        }
    }
}
