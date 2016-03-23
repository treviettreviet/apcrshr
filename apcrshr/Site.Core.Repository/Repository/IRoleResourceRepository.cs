using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IRoleResourceRepository : IRepository<RoleResource>
    {
        RoleResource FindByID(string resourceID, string roleID);
        void Delete(string resourceID, string roleID);
    }
}
