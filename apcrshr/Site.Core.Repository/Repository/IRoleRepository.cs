using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IRoleRepository : IRepository<Role>
    {
        IList<Role> FindAllAvailables(string adminID);
        IList<Role> FindAllAssignedRoles(string adminID);
        IList<Role> FindAllExceptAdministrator();
    }
}
