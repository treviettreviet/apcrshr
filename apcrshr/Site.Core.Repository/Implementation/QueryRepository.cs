using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class QueryRepository
    {
        public void ExecuteQuery(string query)
        {
            using (APCRSHREntities context = new APCRSHREntities())
            {
                context.Database.ExecuteSqlCommand(query);
            }
        }
    }
}
