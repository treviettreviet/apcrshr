using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IVideoRepository : IRepository<Video>
    {
        Video FindByActionURL(string actionURL);
        IList<Video> FindTop(int top, string language);
        Tuple<int, IList<Video>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<Video>> FindAll(int pageSize, int pageIndex, string language);
        Tuple<int, IList<Video>> FindAllRelated(DateTime date, int pageSize, int pageIndex, string language);
        IList<Video> Search(string key);
    }
}
