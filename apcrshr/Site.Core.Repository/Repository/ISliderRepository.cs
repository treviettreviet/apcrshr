using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        Slider FindByActionURL(string URL);
        Slider FindByTitle(string title);
        IList<Slider> FindTop(int top);
        Tuple<int, IList<Slider>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<Slider>> FindAllRelated(DateTime date, int pageSize, int pageIndex);
        IList<Slider> Search(string key);
    }
}
