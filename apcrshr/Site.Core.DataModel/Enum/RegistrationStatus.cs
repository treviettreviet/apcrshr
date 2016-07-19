using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Enum
{
    public enum RegistrationStatus
    {
        /// <summary>
        /// Account created with basic information
        /// </summary>
        Created = 0,
        /// <summary>
        /// Account actived with confimation email confirmed
        /// </summary>
        Actived = 1
    }
}
