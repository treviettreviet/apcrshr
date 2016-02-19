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

        private static IMapper mapper;

        public IMapper Mapper
        {
            get
            {
                if (mapper != null)
                {
                    return mapper;
                }
                else
                {
                    Create();
                    return mapper;
                }
            }
        }

        private MapperUtil()
        {
            Create();
        }

        private static void Create()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //View model mapper
                cfg.CreateMap<AdminModel, Admin>();
                cfg.CreateMap<NewsModel, News>();
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<SubscriberModel, Subscriber>();
                cfg.CreateMap<MenuModel, Menu>();
                cfg.CreateMap<ArticleModel, Article>();
                cfg.CreateMap<UploadModel, Upload>();

                //Entity model mapper
                cfg.CreateMap<Admin, AdminModel>();
                cfg.CreateMap<News, NewsModel>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Subscriber, SubscriberModel>();
                cfg.CreateMap<Menu, MenuModel>()
                    .ForMember(m => m.SubMenus, c => c.MapFrom(m => m.Menu1))
                    .ForMember(m => m.Parent, c => c.MapFrom(m => m.Menu2));
                cfg.CreateMap<Article, ArticleModel>();
                cfg.CreateMap<Upload, UploadModel>();
            });

            mapper = config.CreateMapper();
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
}
