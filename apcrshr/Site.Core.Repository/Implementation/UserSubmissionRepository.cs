using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class UserSubmissionRepository : IUserSubmissionRepository
    {
        public object Insert(UserSubmission item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.UserSubmissions.Add(item);
                context.SaveChanges();
                return item.SubmitID;
            }
        }

        public void Update(UserSubmission item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var submit = context.UserSubmissions.Where(a => a.SubmitID.Equals(item.SubmitID)).SingleOrDefault();
                if (submit != null)
                {
                    submit.SubmissionNumber = item.SubmissionNumber;
                    submit.UserID = item.UserID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Submit id {0} invalid", item.SubmitID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var submit = context.UserSubmissions.Where(a => a.SubmitID.Equals(_id)).SingleOrDefault();
                if (submit != null)
                {
                    context.UserSubmissions.Remove(submit);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Submit id {0} invalid", id));
                }
            }
        }

        public UserSubmission FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.UserSubmissions.Where(a => a.SubmitID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<UserSubmission> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.UserSubmissions.ToList();
            }
        }

        public IList<UserSubmission> FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.UserSubmissions.Where(s => s.UserID.Equals(userID)).ToList();
            }
        }


        public UserSubmission FindBySubmissionNumber(string submissionNumber)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.UserSubmissions.Where(a => a.SubmissionNumber.Equals(submissionNumber)).SingleOrDefault();
            }
        }
    }
}
