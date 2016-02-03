using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin FindByUserName(string username);
        Admin Login(string username, string password);
        IList<Admin> GetAdminsExceptMe(string adminID);
    }
}
