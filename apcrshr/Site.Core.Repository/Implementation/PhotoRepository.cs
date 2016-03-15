using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class PhotoRepository : IPhotoRepository
    {
        public Photo FindByActionURL(string actionURL)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Photos.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
            }
        }

        public Tuple<int, IList<Photo>> FindAll(int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var count = context.Photos.Count();
                var photo = context.Photos.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return Tuple.Create<int, IList<Photo>>(count, photo);
            }
        }

        public Tuple<int, IList<Photo>> FindAllRelated(string AlbumID, DateTime date, int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var photo = context.Photos.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date && n.AlbumID.Equals(AlbumID)).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Photos.OrderByDescending(n => n.CreatedDate).Where(n => n.CreatedDate < date && n.AlbumID.Equals(AlbumID)).Count();
                return Tuple.Create<int, IList<Photo>>(count, photo);
            }
        }

        public IList<Photo> Search(string key)
        {
            throw new NotImplementedException();
        }

        public object Insert(Photo item)
        {
            throw new NotImplementedException();
        }

        public void Update(Photo item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Photo FindByID(object id)
        {
            throw new NotImplementedException();
        }

        public IList<Photo> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
