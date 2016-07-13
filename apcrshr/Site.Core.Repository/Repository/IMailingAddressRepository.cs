using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IMailingAddressRepository : IRepository<MailingAddress>
    {
        IList<MailingAddress> FindByUserID(string userID);
    }
}
