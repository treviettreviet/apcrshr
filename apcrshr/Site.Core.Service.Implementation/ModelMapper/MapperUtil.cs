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
                cfg.CreateMap<ConferenceDeclarationModel, ConferenceDeclaration>();
                cfg.CreateMap<ImportantDeadlineModel, ImportantDeadline>();
                cfg.CreateMap<VideoModel, Video>();
                cfg.CreateMap<PresentationModel, Presentation>();
                cfg.CreateMap<AlbumModel, Album>();
                cfg.CreateMap<PhotoModel, Photo>();
                cfg.CreateMap<RoleModel, Role>();
                cfg.CreateMap<ResourceModel, Resource>();
                cfg.CreateMap<SliderModel, Slider>();
                cfg.CreateMap<MailingAddressModel, MailingAddress>();
                cfg.CreateMap<SessionModel, Session>();
                cfg.CreateMap<MainScholarshipModel, MainScholarship>();
                cfg.CreateMap<EducationModel, Education>();
                cfg.CreateMap<ExperienceModel, Experience>();
                cfg.CreateMap<LeaderShipModel, LeaderShip>();
                cfg.CreateMap<PublicationModel, Publication>();
                cfg.CreateMap<TrainingModel, Training>();
                cfg.CreateMap<YouthScholarshipModel, YouthScholarship>();
                cfg.CreateMap<UserSubmissionModel, UserSubmission>();
                cfg.CreateMap<PaymentModel, Payment>();
                cfg.CreateMap<LogisticScheduleModel, LogisticSchedule>();

                //Entity model mapper
                cfg.CreateMap<Admin, AdminModel>();
                cfg.CreateMap<News, NewsModel>();
                cfg.CreateMap<User, UserModel>()
                    .ForMember(u => u.ParticipantType, m => m.MapFrom(u => u.MailingAddresses.FirstOrDefault().ParticipantType));
                cfg.CreateMap<Subscriber, SubscriberModel>();
                cfg.CreateMap<Menu, MenuModel>()
                    .ForMember(m => m.SubMenus, c => c.MapFrom(m => m.Menu1))
                    .ForMember(m => m.Parent, c => c.MapFrom(m => m.Menu2));
                cfg.CreateMap<Article, ArticleModel>();
                cfg.CreateMap<Upload, UploadModel>();
                cfg.CreateMap<ConferenceDeclaration, ConferenceDeclarationModel>();
                cfg.CreateMap<ImportantDeadline, ImportantDeadlineModel>();
                cfg.CreateMap<Video, VideoModel>();
                cfg.CreateMap<Presentation, PresentationModel>();
                cfg.CreateMap<Album, AlbumModel>()
                    .ForMember(a => a.Photos, c => c.MapFrom(a => a.Photos));
                cfg.CreateMap<Photo, PhotoModel>();
                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<Resource, ResourceModel>();
                cfg.CreateMap<Slider, SliderModel>();
                cfg.CreateMap<MailingAddress, MailingAddressModel>();
                cfg.CreateMap<Session, SessionModel>();
                cfg.CreateMap<MainScholarship, MainScholarshipModel>();
                cfg.CreateMap<Education, EducationModel>();
                cfg.CreateMap<Experience, ExperienceModel>();
                cfg.CreateMap<LeaderShip, LeaderShipModel>();
                cfg.CreateMap<Publication, PublicationModel>();
                cfg.CreateMap<Training, TrainingModel>();
                cfg.CreateMap<YouthScholarship, YouthScholarshipModel>();
                cfg.CreateMap<UserSubmission, UserSubmissionModel>();
                cfg.CreateMap<Payment, PaymentModel>();
                cfg.CreateMap<LogisticSchedule, LogisticScheduleModel>();
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
