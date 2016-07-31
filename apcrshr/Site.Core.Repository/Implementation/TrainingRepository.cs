using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class TrainingRepository : ITrainingRepository
    {
        public object Insert(Training item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Trainings.Add(item);
                context.SaveChanges();
                return item.TrainingID;
            }
        }

        public void Update(Training item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var training = context.Trainings.Where(a => a.TrainingID.Equals(item.TrainingID)).SingleOrDefault();
                if (training != null)
                {
                    training.Country = item.Country;
                    training.NameOfCourse = item.NameOfCourse;
                    training.TrainingEnd = item.TrainingEnd;
                    training.TrainingNow = item.TrainingNow;
                    training.TrainingStart = item.TrainingStart;
                    training.YouthScholarshipID = item.YouthScholarshipID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Training id {0} invalid", item.YouthScholarshipID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var training = context.Trainings.Where(a => a.TrainingID.Equals(_id)).SingleOrDefault();
                if (training != null)
                {
                    context.Trainings.Remove(training);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Training id {0} invalid", id));
                }
            }
        }

        public Training FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Trainings.Where(a => a.TrainingID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Training> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Trainings.ToList();
            }
        }
    }
}
