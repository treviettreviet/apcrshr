using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Repository
{
    public interface IConferenceDataRepository : IRepository<ConferenceData>
    {
        ConferenceData FindByActionURL(string actionURL);
        IList<ConferenceData> FindTop(int top, string language);
        Tuple<int, IList<ConferenceData>> FindAll(int pageSize, int pageIndex);
        Tuple<int, IList<ConferenceData>> FindAll(int pageSize, int pageIndex, string language);
        Tuple<int, IList<ConferenceData>> FindAll(int pageSize, int pageIndex, string language, string menuID);
        Tuple<int, IList<ConferenceData>> FindAllRelated(DateTime date, int pageSize, int pageIndex, string language);
        IList<ConferenceData> Search(string key);
    }
}
