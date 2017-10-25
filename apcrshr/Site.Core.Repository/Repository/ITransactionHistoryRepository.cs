using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface ITransactionHistoryRepository : IRepository<TransactionHistory>
    {
        IList<TransactionHistory> FindByUserID(string userID);
        IList<TransactionHistory> FindByTransactionReference(long referenceId);
    }
}
