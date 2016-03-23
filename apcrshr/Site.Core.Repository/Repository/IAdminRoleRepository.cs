using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IAdminRoleRepository : IRepository<AdminRole>
    {
        AdminRole FindByID(string adminID, string roleID);
        void Delete(string adminID, string roleID);
    }
}
