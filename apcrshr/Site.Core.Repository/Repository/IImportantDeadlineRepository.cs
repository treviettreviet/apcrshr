using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IImportantDeadlineRepository
    {
        ImportantDeadline FindByActionURL(string actionURL);
        ImportantDeadline FindByID(object id);
        ImportantDeadline FindByTitle(string title);
        IList<ImportantDeadline> FindTop(int top);
        IList<ImportantDeadline> Search(string key);
        IList<ImportantDeadline> FindAll();
        Tuple<int, IList<ImportantDeadline>> FindAll(int pageSize, int pageIndex);
    }
}
