using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository.Implementation
{
    public class ConferenceDataRepository : IConferenceDataRepository
    {
        public ConferenceData FindByActionURL(string actionURL)
        {
            throw new NotImplementedException();
        }

        public IList<ConferenceData> FindTop(int top, string language)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, IList<ConferenceData>> FindAll(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, IList<ConferenceData>> FindAll(int pageSize, int pageIndex, string language)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, IList<ConferenceData>> FindAll(int pageSize, int pageIndex, string language, string menuID)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, IList<ConferenceData>> FindAllRelated(DateTime date, int pageSize, int pageIndex, string language)
        {
            throw new NotImplementedException();
        }

        public IList<ConferenceData> Search(string key)
        {
            throw new NotImplementedException();
        }

        public object Insert(ConferenceData item)
        {
            throw new NotImplementedException();
        }

        public void Update(ConferenceData item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public ConferenceData FindByID(object id)
        {
            throw new NotImplementedException();
        }

        public IList<ConferenceData> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
