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
        /// Accounted created with mailing infomation
        /// </summary>
        MailingCreated = 1,
        /// <summary>
        /// Account created with registration email sent
        /// </summary>
        RegisterCompleted = 2,
        /// <summary>
        /// Account actived with confimation email confirmed
        /// </summary>
        Actived = 3,
        /// <summary>
        /// Account is locked
        /// </summary>
        Locked = 4,
        /// <summary>
        /// Email duplicated need to be confirmed
        /// </summary>
        EmailDuplicated = 5
    }
}
