using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
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
        public CreateSubscriberResponse CreateSubscriber(SubscriberModel subscriber)
        {
            using (ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository())
            {
                try
                {
                    var _sub = Mapper.Map<SubscriberModel, Subscriber>(subscriber);
                    object id = subscriberRepository.Insert(_sub);
                    return new CreateSubscriberResponse
                    {
                        SubscriberID = _sub.SubscriberID,
                        ErrorCode = (int)ErrorCode.None,
                        Message = AdminResource.msg_create_success
                    };
                }
                catch (Exception ex)
                {
                    return new CreateSubscriberResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public BaseResponse DeleteSubscriber(string id)
        {
            using (ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository())
            {
                try
                {
                    subscriberRepository.Delete(id);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_delete_success
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
        }

        public BaseResponse UpdateSubscriber(SubscriberModel subscriber)
        {
            using (ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository())
            {
                try
                {
                    var _sub = Mapper.Map<SubscriberModel, Subscriber>(subscriber);
                    subscriberRepository.Update(_sub);
                    return new BaseResponse
                    {
                        ErrorCode = (int)ErrorCode.None,
                        Message = Resources.AdminResource.msg_update_success
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
        }

        public GetAllSubscribersResponse GetSubscribers()
        {
            using (ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository())
            {
                try
                {
                    IList<Subscriber> result = subscriberRepository.FindAll();
                    var _subs = result.Select(m => Mapper.Map<Subscriber, SubscriberModel>(m)).ToList();
                    return new GetAllSubscribersResponse
                    {
                        Subscribers = _subs,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetAllSubscribersResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }

        public GetSubscriberResponse FindSubscriberByID(string id)
        {
            using (ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository())
            {
                try
                {
                    Subscriber sub = subscriberRepository.FindByID(id);
                    var _sub = Mapper.Map<Subscriber, SubscriberModel>(sub);
                    return new GetSubscriberResponse
                    {
                        Subscriber = _sub,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetSubscriberResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }
        
        public GetSubscriberResponse FindSubscriberByEmail(string email)
        {
            using (ISubscriberRepository subscriberRepository = RepositoryClassFactory.GetInstance().GetSubscriberRepository())
            {
                try
                {
                    Subscriber sub = subscriberRepository.FindByEmail(email);
                    var _sub = Mapper.Map<Subscriber, SubscriberModel>(sub);
                    return new GetSubscriberResponse
                    {
                        Subscriber = _sub,
                        ErrorCode = (int)ErrorCode.None,
                        Message = string.Empty
                    };
                }
                catch (Exception ex)
                {
                    return new GetSubscriberResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = ex.Message
                    };
                }
            }
        }
    }
}
