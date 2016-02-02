using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Implementation.Convertor
{
    public interface IConvertor <T, V>
    {
        IList<T> Convert(IList<V> list);
        IList<V> Convert(IList<T> list);
    }
}
