using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Photo FindByActionURL(string actionURL);
        Tuple<int, IList<Photo>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<Photo>> FindAllRelated(string AlbumID, DateTime date, int pageSize, int pageIndex);
        IList<Photo> Search(string key);
    }
}
