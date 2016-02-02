using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Site.Core.DataModel.Model;
using Site.Core.Repository;

namespace Site.Core.Service.Implementation.ModelMapper
{
    public class MapperUtil
    {
        private static MapperUtil Instance;

        private MapperUtil()
        {
            //View model mapper
            Mapper.CreateMap<AdminModel, Admin>();
            Mapper.CreateMap<NewsModel, News>();
            Mapper.CreateMap<UserModel, User>();
            Mapper.CreateMap<SubscriberModel, Subscriber>();

            //Entity model mapper
            Mapper.CreateMap<Admin, AdminModel>();
            Mapper.CreateMap<News, NewsModel>();
            Mapper.CreateMap<User, UserModel>();
            Mapper.CreateMap<Subscriber, SubscriberModel>();
        }

        public static MapperUtil CreateMapper()
        {
            if (Instance == null)
            {
                Instance = new MapperUtil();
            }
            return Instance;
        }
    }

    public class PartResolver<V, T> : ValueResolver<V, T>
    {
        protected override T ResolveCore(V source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
