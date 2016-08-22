using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IImportantDeadlineRepository : IRepository<ImportantDeadline>
    {
        ImportantDeadline FindByType(int type);
        ImportantDeadline FindByActionURL(string actionURL);
        ImportantDeadline FindByTitle(string title);
        IList<ImportantDeadline> FindTop(int top);
        IList<ImportantDeadline> FindTop(int top, DateTime date);
        IList<ImportantDeadline> Search(string key);
        Tuple<int, IList<ImportantDeadline>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<ImportantDeadline>> FindAllRelated(DateTime date, int pageSize, int pageIndex);
    }
}
