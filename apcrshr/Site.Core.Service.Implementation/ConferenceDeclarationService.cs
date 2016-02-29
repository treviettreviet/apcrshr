using Site.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Implementation
{
    public class ConferenceDeclarationService : IConferenceDeclarationService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.ConferenceDeclarationModel> FindConferenceByID(string id)
        {
            throw new NotImplementedException();
        }

        public DataModel.Response.BaseResponse DeleteConference(string id)
        {
            throw new NotImplementedException();
        }

        public DataModel.Response.BaseResponse UpdateConference(DataModel.Model.ConferenceDeclarationModel conference)
        {
            throw new NotImplementedException();
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ConferenceDeclarationModel> GetConference()
        {
            throw new NotImplementedException();
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ConferenceDeclarationModel> GetConference(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public DataModel.Response.InsertResponse CreateConference(DataModel.Model.ConferenceDeclarationModel conference)
        {
            throw new NotImplementedException();
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.ConferenceDeclarationModel> GetConferenceByID(string conferenceID)
        {
            throw new NotImplementedException();
        }
    }
}
