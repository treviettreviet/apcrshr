using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface INewsRepository : IRepository<News>
    {
        News FindByActionURL(string actionURL);
        IList<News> FindTop(int top, string language);
        Tuple<int, IList<News>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<News>> FindAll(int pageSize, int pageIndex, string language);
        Tuple<int, IList<News>> FindAll(int pageSize, int pageIndex, string language, string menuID);
        Tuple<int, IList<News>> FindAllRelated(string menuID, DateTime date, int pageSize, int pageIndex, string language);
        IList<News> Search(string key);
    }
}
