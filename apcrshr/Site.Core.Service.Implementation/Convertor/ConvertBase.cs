using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Implementation.Convertor
{
    public abstract class ConvertBase <T, V> : IConvertor<T, V>
    {

        public IList<T> Convert(IList<V> list)
        {
            IList<T> result = new List<T>();
            foreach (V item in list)
            {
                result.Add(Convert(item));
            }
            return result;
        }

        public IList<V> Convert(IList<T> list)
        {
            IList<V> result = new List<V>();
            foreach (T item in list)
            {
                result.Add(Convert(item));
            }
            return result;
        }

        public abstract T Convert(V item);
        public abstract V Convert(T item);
    }
}
