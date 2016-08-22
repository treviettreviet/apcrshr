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
        /// The user isn't paid
        /// </summary>
        PaymentNotCompleted = 1,
        /// <summary>
        /// The user has paid
        /// </summary>
        PaymentCompleted = 2
    }
}
