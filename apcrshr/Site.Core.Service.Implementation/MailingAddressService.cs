using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation.ModelMapper;

namespace Site.Core.Service.Implementation
{
    public class MailingAddressService : IMailingAddressService
    {
        public DataModel.Response.InsertResponse CreateMailingAddress(DataModel.Model.MailingAddressModel mailing)
        {
            try
            {
                IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                object id = mailingRepository.Insert(MapperUtil.CreateMapper().Mapper.Map<MailingAddressModel, MailingAddress>(mailing));
                return new InsertResponse
                {
                    InsertID = id.ToString(),
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_create_success
                };
            }
            catch (Exception ex)
            {
                return new InsertResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.MailingAddressModel> FindMailingAddressByID(string Id)
        {
            try
            {
                IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                MailingAddress mailing = mailingRepository.FindByID(Id);
                var _mailing = MapperUtil.CreateMapper().Mapper.Map<MailingAddress, MailingAddressModel>(mailing);
                return new FindItemReponse<MailingAddressModel>
                {
                    Item = _mailing,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<MailingAddressModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.MailingAddressModel> FindMailingAddressByUser(string userID)
        {
            try
            {
                IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                IList<MailingAddress> mailings = mailingRepository.FindByUserID(userID);
                var _mailings = mailings.Select(n => MapperUtil.CreateMapper().Mapper.Map<MailingAddress, MailingAddressModel>(n)).ToList();
                return new FindAllItemReponse<MailingAddressModel>
                {
                    Items = _mailings,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<MailingAddressModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse DeleteMailingAddress(string id)
        {
            try
            {
                IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                mailingRepository.Delete(id);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_delete_success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse UpdateMailingAddress(DataModel.Model.MailingAddressModel mailing)
        {
            try
            {
                IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                MailingAddress _mailing = MapperUtil.CreateMapper().Mapper.Map<MailingAddressModel, MailingAddress>(mailing);
                mailingRepository.Update(_mailing);
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.None,
                    Message = Resources.Resource.msg_update_success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.MailingAddressModel> GetMailingAddresses()
        {
            try
            {
                IMailingAddressRepository mailingRepository = RepositoryClassFactory.GetInstance().GetMailingAddressRepository();
                IList<MailingAddress> mailings = mailingRepository.FindAll();
                var _mailings = mailings.Select(n => MapperUtil.CreateMapper().Mapper.Map<MailingAddress, MailingAddressModel>(n)).ToList();
                return new FindAllItemReponse<MailingAddressModel>
                {
                    Items = _mailings,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<MailingAddressModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
