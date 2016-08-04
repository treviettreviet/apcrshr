using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User Login(string username, string password);

        IList<User> GetUserExceptMe(string userID);

        User FindByEmail(string email);

        User FindByUserName(string username);

        void ChangePassword(string userID, string newPassword);
    }
}
