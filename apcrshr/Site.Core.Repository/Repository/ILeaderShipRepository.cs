﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface ILeaderShipRepository : IRepository<LeaderShip>
    {
        IList<LeaderShip> FindByOrganization(string organization);
        IList<LeaderShip> FindByYouthScholarshipID(string scholarshipID);
    }
}
