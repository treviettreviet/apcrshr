using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IPublicationRepository : IRepository<Publication>
    {
        IList<Publication> FindByTitle(string title);
    }
}
