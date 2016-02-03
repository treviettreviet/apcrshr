using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.DataModel.Response
{
    public class FindAllItemReponse<T> : BaseResponse
    {
        public IList<T> Items { get; set; }

        public int Count { get; set; }
    }
}
