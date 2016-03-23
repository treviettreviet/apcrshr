using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IResourceRepository : IRepository<Resource>
    {
        IList<Resource> FindAllAvailables(string roleID);
        IList<Resource> FindAllAssignedResources(string roleID);
        Resource FindAuthorizedResource(string adminID, string resourceURL);
    }
}
