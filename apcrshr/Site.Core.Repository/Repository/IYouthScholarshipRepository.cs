﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IYouthScholarshipRepository : IRepository<YouthScholarship>
    {
        YouthScholarship FindByUserID(string userID);
    }
}
