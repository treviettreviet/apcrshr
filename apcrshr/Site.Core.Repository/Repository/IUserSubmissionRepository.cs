﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IUserSubmissionRepository : IRepository<UserSubmission>
    {
        IList<UserSubmission> FindByUserID(string userID);
        UserSubmission FindBySubmissionNumber(string submissionNumber);
    }
}
