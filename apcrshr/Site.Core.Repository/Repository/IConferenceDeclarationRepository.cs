using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IConferenceDeclarationRepository : IRepository<ConferenceDeclaration>
    {
        Tuple<int, IList<ConferenceDeclaration>> FindAll(int pageSize, int pageIndex);
        IList<ConferenceDeclaration> Search(string key);
    }
}
