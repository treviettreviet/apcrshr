using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Enum
{
    public enum ScholarshipStatus
    {
        /// <summary>
        /// Scholarship is not reviewed
        /// </summary>
        Submitted = 0,
        /// <summary>
        /// Scholarship is reviewed
        /// </summary>
        Reviewed = 1,
        /// <summary>
        /// Scholarship has rejected
        /// </summary>
        Rejected = 2
    }
}
