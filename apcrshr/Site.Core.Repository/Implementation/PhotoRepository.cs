using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public IList<Photo> Search(string key)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Photos.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
                new SqlParameter("@stringToFind", key),
                new SqlParameter("@schema", "dbo"),
                new SqlParameter("@table", "Photo")).ToList();
            }
        }

        public object Insert(Photo item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Photos.Add(item);
                context.SaveChanges();
                return item.PhotoID;
            }
        }

        public void Update(Photo item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var result = context.Photos.Where(n => n.PhotoID.Equals(item.PhotoID)).SingleOrDefault();
                if (result != null)
                {
                    result.Title = item.Title;
                    if (!string.IsNullOrEmpty(item.ActionURL))
                    {
                        result.ActionURL = item.ActionURL;
                    }
                    if (!string.IsNullOrEmpty(item.ImageURL))
                    {
                        result.ImageURL = item.ImageURL;
                    }
                    result.Description = item.Description;
                    result.AlbumID = item.AlbumID;
                    result.UpdatedBy = item.UpdatedBy;
                    result.UpdatedDate = DateTime.Now;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Photo's id {0} invalid", item.PhotoID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var item = context.Photos.Where(n => n.PhotoID.Equals(_id)).SingleOrDefault();
                if (item != null)
                {
                    context.Photos.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Photo's id {0} invalid", item.PhotoID));
                }
            }
        }

        public Photo FindByID(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                return context.Photos.Where(a => a.PhotoID.Equals(_id)).SingleOrDefault();
            }
        }

        public IList<Photo> FindAll()
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                return context.Photos.OrderByDescending(n => n.CreatedDate).ToList();
            }
        }


        public Tuple<int, IList<Photo>> FindByAlbum(string AlbumID, int pageSize, int pageIndex)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var photo = context.Photos.OrderByDescending(n => n.CreatedDate).Where(n => n.AlbumID.Equals(AlbumID)).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                var count = context.Photos.OrderByDescending(n => n.CreatedDate).Where(n => n.AlbumID.Equals(AlbumID)).Count();
                return Tuple.Create<int, IList<Photo>>(count, photo);
            }
        }
    }
}
