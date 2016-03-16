using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation.ModelMapper;
using Site.Core.Service.Implementation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Implementation
{
    public class SubscriberService : ISubscriberService
    {
        public InsertResponse CreateSubscriber(SubscriberModel subscriber)
        {
            try
            {
                ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository();
                var _sub = MapperUtil.CreateMapper().Mapper.Map<SubscriberModel, Subscriber>(subscriber);
                object id = subscriberRepository.Insert(_sub);
                return new InsertResponse
                {
                    InsertID = _sub.SubscriberID,
                    ErrorCode = (int)ErrorCode.None,
                    Message = Site.Core.Service.Implementation.Resources.Resource.msg_create_success
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

        public BaseResponse DeleteSubscriber(string id)
        {
            try
            {
                ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository();
                subscriberRepository.Delete(id);
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

        public BaseResponse UpdateSubscriber(SubscriberModel subscriber)
        {
            try
            {
                ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository();
                var _sub = MapperUtil.CreateMapper().Mapper.Map<SubscriberModel, Subscriber>(subscriber);
                subscriberRepository.Update(_sub);
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

        public FindAllItemReponse<SubscriberModel> GetSubscribers()
        {
            try
            {
                ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository();
                IList<Subscriber> result = subscriberRepository.FindAll();
                var _subs = result.Select(m => MapperUtil.CreateMapper().Mapper.Map<Subscriber, SubscriberModel>(m)).ToList();
                return new FindAllItemReponse<SubscriberModel>
                {
                    Items = _subs,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<SubscriberModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<SubscriberModel> FindSubscriberByID(string id)
        {
            try
            {
                ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository();
                Subscriber sub = subscriberRepository.FindByID(id);
                var _sub = MapperUtil.CreateMapper().Mapper.Map<Subscriber, SubscriberModel>(sub);
                return new FindItemReponse<SubscriberModel>
                {
                    Item = _sub,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<SubscriberModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public FindItemReponse<SubscriberModel> FindSubscriberByEmail(string email)
        {
            try
            {
                ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository();
                Subscriber sub = subscriberRepository.FindByEmail(email);
                var _sub = MapperUtil.CreateMapper().Mapper.Map<Subscriber, SubscriberModel>(sub);
                return new FindItemReponse<SubscriberModel>
                {
                    Item = _sub,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<SubscriberModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
