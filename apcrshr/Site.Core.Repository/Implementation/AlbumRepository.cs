using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class AlbumRepository : IAlbumRepository
    {
        public Album FindByActionURL(string actionURL)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Albums.Where(a => a.ActionURL.Equals(actionURL)).SingleOrDefault();
        }

        public object Insert(Album item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Albums.Add(item);
                context.SaveChanges();
                return item.AlbumID;
            }
        }

        public void Update(Album item)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var result = context.Albums.Where(n => n.AlbumID.Equals(item.AlbumID)).SingleOrDefault();
                if (result != null)
                {
                    result.Title = item.Title;
                    if (!string.IsNullOrEmpty(item.ActionURL))
                    {
                        result.ActionURL = item.ActionURL;
                    }
                    result.Description = item.Description;
                    result.UpdatedBy = item.UpdatedBy;
                    result.UpdatedDate = DateTime.Now;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Album's id {0} invalid", item.AlbumID));
                }
            }
        }

        public void Delete(object id)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                var _id = id.ToString();
                var item = context.Albums.Where(n => n.AlbumID.Equals(_id)).SingleOrDefault();
                if (item != null)
                {
                    context.Albums.Remove(item);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Album's id {0} invalid", item.AlbumID));
                }
            }
        }

        public Album FindByID(object id)
        {
            APCRSHREntities context = new APCRSHREntities();
            var _id = id.ToString();
            return context.Albums.Where(a => a.AlbumID.Equals(_id)).SingleOrDefault();
        }

        public IList<Album> FindAll()
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Albums.OrderByDescending(n => n.CreatedDate).ToList();
        }


        public Tuple<int, IList<Album>> FindAll(int pageSize, int pageIndex)
        {
            APCRSHREntities context = new APCRSHREntities();
            var count = context.Albums.Count();
            var albums = context.Albums.OrderByDescending(n => n.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Tuple.Create<int, IList<Album>>(count, albums);
        }


        public IList<Album> Search(string key)
        {
            APCRSHREntities context = new APCRSHREntities();
            return context.Albums.SqlQuery("exec sp_FindStringInTable @stringToFind,@schema,@table",
            new SqlParameter("@stringToFind", key),
            new SqlParameter("@schema", "dbo"),
            new SqlParameter("@table", "Album")).ToList();
        }
    }
}
