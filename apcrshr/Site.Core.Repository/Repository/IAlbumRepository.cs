﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Album FindByActionURL(string actionURL);
        Tuple<int, IList<Album>> FindAll(int pageSize, int pageIndex);
        IList<Album> Search(string key);
    }
}
