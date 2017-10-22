using Site.Core.Repository.Implementation;
using Site.Core.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Repository
{
    public class RepositoryClassFactory
    {
        private static RepositoryClassFactory _Instance;

        private RepositoryClassFactory() { }

        public static RepositoryClassFactory GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new RepositoryClassFactory();
            }
            return _Instance;
        }

        public IAdminRepository GetAdminRepository()
        {
            return new AdminRepository();
        }

        public INewsRepository GetNewsRepository()
        {
            return new NewsRepository();
        }

        public IUserRepository GetUserRepository()
        {
            return new UserRepository();
        }

        public IUserSessionRepository GetUserSessionRepository()
        {
            return new UserSessionRepository();
        }

        public ISubscriberRepository GetSubscriberRepository()
        {
            return new SubscriberRepository();
        }

        public IMenuRepository GetMenuRepository()
        {
            return new MenuRepository();
        }

        public IArticleRepository GetArticleRepository()
        {
            return new ArticleRepository();
        }

        public IUploadRepository GetUploadRepository()
        {
            return new UploadRepository();
        }
        public IImportantDeadlineRepository GetImportantDeadlineRepository()
        {
            return new ImportantDeadlineRepository();
        }
        public IConferenceDeclarationRepository GetConferenceDeclarationRepository()
        {
            return new ConferenceDeclarationRepository();
        }
        public IVideoRepository GetVideoRepository()
        {
            return new VideoRepository();
        }
        public IPresentationRepository GetPresentationRepository()
        {
            return new PresentationRepository();
        }
        public IAlbumRepository GetAlbumRepository()
        {
            return new AlbumRepository();
        }
        public IPhotoRepository GetPhotoRepository()
        {
            return new PhotoRepository();
        }
        public IRoleRepository GetRoleRepository()
        {
            return new RoleRepository();
        }
        public IResourceRepository GetResourceRepository()
        {
            return new ResourceRepository();
        }
        public IAdminRoleRepository GetAdminRoleRepository()
        {
            return new AdminRoleRepository();
        }
        public IRoleResourceRepository GetRoleResourceRepository()
        {
            return new RoleResourceRepository();
        }
        public ISliderRepository GetSliderRepository()
        {
            return new SliderRepository();
        }
        public IMailingAddressRepository GetMailingAddressRepository()
        {
            return new MailingAddressRepository();
        }

        public ISessionRepository GetSessionRepository()
        {
            return new SessionRepository();
        }

        public IEducationRepository GetEducationRepository()
        {
            return new EducationRepository();
        }

        public IExperienceRepository GetExperienceRepository()
        {
            return new ExperienceRepository();
        }

        public ILeaderShipRepository GetLeaderShipRepository()
        {
            return new LeaderShipRepository();
        }

        public IMainScholarshipRepository GetMainScholarshipRepository()
        {
            return new MainScholarshipRepository();
        }

        public IPublicationRepository GetPublicationRepository()
        {
            return new PublicationRepository();
        }

        public ITrainingRepository GetTrainingRepository()
        {
            return new TrainingRepository();
        }

        public IYouthScholarshipRepository GetYouthShcolarshipReoisitory()
        {
            return new YouthScholarshipRepository();
        }

        public IUserSubmissionRepository GetUserSubmissionRepository()
        {
            return new UserSubmissionRepository();
        }

        public IPaymentRepository GetPaymentRepository()
        {
            return new PaymentRepository();
        }

        public ILogisticSheduleRepository GetLogisticRepository()
        {
            return new LogisticSheduleRepository();
        }

        public ITransactionHistoryRepository GetTransactionHistoryRepository()
        {
            return new TransactionHistoryRepository();
        }
    }
}
