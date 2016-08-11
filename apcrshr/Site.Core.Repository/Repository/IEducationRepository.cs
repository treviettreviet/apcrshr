using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IEducationRepository : IRepository<Education>
    {
        IList<Education> FindByMainCourseStudy(string mainCourseStudy);
        IList<Education> FindByYouthScholarshipID(string scholarshipID);
    }
}
