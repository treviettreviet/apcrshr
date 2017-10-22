﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Enum
{
    public enum PaymentStatus
    {
        Initialized = 5,
        Completed = 0,
        Pending = 1,
        Error = 2,
        Cash = 3,
        FreeRegistration = 4
    }
}
