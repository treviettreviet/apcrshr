using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IConferenceDeclarationRepository : IRepository<ConferenceDeclaration>
    {
        ConferenceDeclaration FindByActionURL(string actionURL);
        Tuple<int, IList<ConferenceDeclaration>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<ConferenceDeclaration>> FindAllRelated(DateTime date, int pageSize, int pageIndex);
        IList<ConferenceDeclaration> Search(string key);
    }
}
