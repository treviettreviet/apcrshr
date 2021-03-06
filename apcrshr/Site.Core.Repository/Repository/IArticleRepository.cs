﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IArticleRepository : IRepository<Article>
    {
        IList<Article> FindByMenuID(string menuID);
        Article FindByActionURL(string actionURL);
        Tuple<int, IList<Article>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<Article>> FindAll(int pageSize, int pageIndex, string language);
        Tuple<int, IList<Article>> FindAll(int pageSize, int pageIndex, string language, string menuID);
        IList<Article> Search(string key);
        IList<Article> FindTopHomeDisplay(int top);
    }
}
