using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class MailingAddressRepository : IMailingAddressRepository
    {
        public object Insert(MailingAddress item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.MailingAddresses.Add(item);
                context.SaveChanges();
                return item.MailingAddressID;
            }
        }

        public void Update(MailingAddress item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var mailing = context.MailingAddresses.Where(a => a.MailingAddressID.Equals(item.MailingAddressID)).SingleOrDefault();
                if (mailing != null)
                {
                    mailing.UserID = item.UserID;
                    mailing.UpdatedDate = DateTime.Now;
                    mailing.ParticipantType = item.ParticipantType;
                    mailing.ParticipateYouth = item.ParticipateYouth;
                    mailing.OriginalNationality = item.OriginalNationality;
                    mailing.CurrentNationality = item.CurrentNationality;
                    mailing.PassportNumber = item.PassportNumber;
                    mailing.TypeOfPassport = item.TypeOfPassport;
                    mailing.Occupation = item.Occupation;
                    mailing.DateOfPassportIssue = item.DateOfPassportIssue;
                    mailing.DateOfPassportExpiry = item.DateOfPassportExpiry;
                    mailing.PassportPhoto1 = item.PassportPhoto1;
                    mailing.PassportPhoto2 = item.PassportPhoto2;
                    mailing.PassportPhoto3 = item.PassportPhoto3;
                    mailing.DetailOfEmbassy = item.DetailOfEmbassy;
                    mailing.NeedVisaSupport = item.NeedVisaSupport;
                    mailing.ActivationCode = item.ActivationCode;
                    mailing.RegistrationNumber = item.RegistrationNumber;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Mailing id {0} invalid", item.MailingAddressID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var mailing = context.MailingAddresses.Where(a => a.MailingAddressID.Equals(_id)).SingleOrDefault();
                if (mailing != null)
                {
                    context.MailingAddresses.Remove(mailing);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Mailing id {0} invalid", id));
                }
            }
        }

        public MailingAddress FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.MailingAddresses.Where(a => a.MailingAddressID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<MailingAddress> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.MailingAddresses.ToList();
            }
        }

        public IList<MailingAddress> FindByUserID(string userID)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.MailingAddresses.Where(m => m.UserID.Equals(userID)).ToList();
            }
        }


        public IList<MailingAddress> FindByActivation(string activationCode)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.MailingAddresses.Where(m => m.ActivationCode.Equals(activationCode)).ToList();
            }
        }
    }
}
