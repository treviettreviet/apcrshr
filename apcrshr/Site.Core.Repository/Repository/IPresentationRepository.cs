using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IPresentationRepository : IRepository<Presentation>
    {
        Presentation FindByActionURL(string actionURL);
        Tuple<int, IList<Presentation>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<Presentation>> FindAllRelated(DateTime date, int pageSize, int pageIndex);
        IList<Presentation> Search(string key);
    }
}
