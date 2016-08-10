using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.Repository.Repository;

namespace Site.Core.Repository.Implementation
{
    public class PublicationRepository : IPublicationRepository
    {
        public object Insert(Publication item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Publications.Add(item);
                context.SaveChanges();
                return item.PublicationID;
            }
        }

        public void Update(Publication item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var publication = context.Publications.Where(a => a.PublicationID.Equals(item.PublicationID)).SingleOrDefault();
                if (publication != null)
                {
                    publication.Source = item.Source;
                    publication.Title = item.Title;
                    publication.URL = item.URL;
                    publication.Year = item.Year;
                    publication.YouthScholarshipID = item.YouthScholarshipID;

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Publication id {0} invalid", item.PublicationID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var publication = context.Publications.Where(a => a.PublicationID.Equals(_id)).SingleOrDefault();
                if (publication != null)
                {
                    context.Publications.Remove(publication);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Publication id {0} invalid", id));
                }
            }
        }

        public Publication FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Publications.Where(a => a.PublicationID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Publication> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Publications.ToList();
            }
        }

        public IList<Publication> FindByTitle(string title)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Publications.Where(p => p.Title.Equals(title)).ToList();
            }
        }
    }
}
