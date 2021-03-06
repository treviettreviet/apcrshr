﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IUploadRepository : IRepository<Upload>
    {
        IList<Upload> FindAll(int top);
    }
}
