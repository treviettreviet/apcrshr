﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IExperienceRepository : IRepository<Experience>
    {
        IList<Experience> FindByOrganization(string organization);
        IList<Experience> FindByYouthScholarshipID(string scholarshipID);
    }
}
