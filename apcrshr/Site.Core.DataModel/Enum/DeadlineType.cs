using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Enum
{
    public enum DeadlineType
    {
        /// <summary>
        /// Custom deadline
        /// </summary>
        Custom = 0,
        /// <summary>
        /// Abstract submission
        /// </summary>
        Abstract = 1,
        /// <summary>
        /// User registration
        /// </summary>
        Registration = 2,
        /// <summary>
        /// Scholarship submit
        /// </summary>
        Scholarship = 3
    }
}
