using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Inserts a new item to the repository
        /// </summary>
        /// <param name="item">An item to be added</param>
        /// <returns>The primary key of the new item</returns>
        object Insert(T item);
        void Update(T item);
        void Delete(object id);
        T FindByID(object id);
        IList<T> FindAll();
    }
}
