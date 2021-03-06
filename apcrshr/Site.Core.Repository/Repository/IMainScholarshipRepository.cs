﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IMainScholarshipRepository : IRepository<MainScholarship>
    {
        IList<MainScholarship> FindByUserID(string userID);
        MainScholarship FindByUserIDAndSubmission(string userID, string submissionNumber);
    }
}
