using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class EducationRepository : IEducationRepository
    {
        public object Insert(Education item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Educations.Add(item);
                context.SaveChanges();
                return item.EducationID;
            }
        }

        public void Update(Education item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var education = context.Educations.Where(a => a.EducationID.Equals(item.EducationID)).SingleOrDefault();
                if (education != null)
                {
                    education.Country = item.Country;
                    education.Degree = item.Degree;
                    education.EducationEnd = item.EducationEnd;
                    education.EducationStart = item.EducationStart;
                    education.MainCourseStudy = item.MainCourseStudy;
                    education.TypeOfTraining = item.TypeOfTraining;
                    education.YouthScholarshipID = item.YouthScholarshipID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Education id {0} invalid", item.EducationID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var education = context.Educations.Where(a => a.EducationID.Equals(_id)).SingleOrDefault();
                if (education != null)
                {
                    context.Educations.Remove(education);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Education id {0} invalid", id));
                }
            }
        }

        public Education FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Educations.Where(a => a.EducationID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Education> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Educations.ToList();
            }
        }
    }
}
