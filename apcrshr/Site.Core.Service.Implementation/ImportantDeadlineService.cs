using AutoMapper;
using Site.Core.DataModel.Enum;
using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Repository;
using Site.Core.Repository.Repository;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation.ModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Site.Core.Service.Implementation
{
    public class ImportantDeadlineService : IImportantDeadlineService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.ImportantDeadlineModel> FindImportantByID(string id)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                ImportantDeadline importantDealine = importantDeadlineRepository.FindByID(id);
                var _importantDeadline = MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(importantDealine);
                return new FindItemReponse<ImportantDeadlineModel>
                {
                    Item = _importantDeadline,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
            
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.ImportantDeadlineModel> FindImportantByActionURL(string url)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                ImportantDeadline importantDealine = importantDeadlineRepository.FindByActionURL(url);
                var _importantDeadline = MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(importantDealine);
                return new FindItemReponse<ImportantDeadlineModel>
                {
                    Item = _importantDeadline,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse UpdateImportantDealine(DataModel.Model.ImportantDeadlineModel importantDeadline)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                ImportantDeadline _importantDeadline = MapperUtil.CreateMapper().Mapper.Map<ImportantDeadlineModel, ImportantDeadline>(importantDeadline);
                importantDeadlineRepository.Update(_importantDeadline);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ImportantDeadlineModel> GetImportantDeadlines()
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                IList<ImportantDeadline> importantDeadline = importantDeadlineRepository.FindAll();
                var _importantDeadline = importantDeadline.Select(i => MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(i)).ToList();
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    Items = _importantDeadline,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ImportantDeadlineModel> GetImportantDeadlines(int pageSize, int pageIndex)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRespository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                var result = importantDeadlineRespository.FindAll(pageSize, pageIndex);
                var _importantDeadline = result.Item2.Select(i => MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(i)).ToList();
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    Count = result.Item1,
                    Items = _importantDeadline,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse CreateImportantDeadline(DataModel.Model.ImportantDeadlineModel importantDeadline)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                ImportantDeadline _importantDeadline = MapperUtil.CreateMapper().Mapper.Map<ImportantDeadlineModel, ImportantDeadline>(importantDeadline);
                object id = importantDeadlineRepository.Insert(_importantDeadline);
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

        public DataModel.Response.BaseResponse DeleteImportantDeadline(string id)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                importantDeadlineRepository.Delete(id);
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

        public DataModel.Response.FindItemReponse<DataModel.Model.ImportantDeadlineModel> GetImportantDeadlineByTitle(string title)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                ImportantDeadline importantDeadline = importantDeadlineRepository.FindByTitle(title);
                var _importantDeadlines = MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(importantDeadline);
                return new FindItemReponse<ImportantDeadlineModel>
                {
                    Item = _importantDeadlines,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.ImportantDeadlineModel> GetTopImportantDeadlines(int top)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                IList<ImportantDeadline> result = importantDeadlineRepository.FindTop(top);
                var _importantDeadline = result.Select(i => MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(i)).ToList();
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    Items = _importantDeadline,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<ImportantDeadlineModel> GetRelatedImportantDeadline(DateTime date, int pageSize, int pageIndex)
        {
            try
            {
                IImportantDeadlineRepository conRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();

                var result = conRepository.FindAllRelated(date, pageSize, pageIndex);
                var _con = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(n)).ToList();
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    Count = result.Item1,
                    Items = _con,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<ImportantDeadlineModel> GetImportantDeadlines(int top)
        {
            try
            {
                IImportantDeadlineRepository importantDeadlineRepository = RepositoryClassFactory.GetInstance().GetImportantDeadlineRepository();
                IList<ImportantDeadline> importantDeadline = importantDeadlineRepository.FindTop(top);
                var _importantDeadline = importantDeadline.Select(i => MapperUtil.CreateMapper().Mapper.Map<ImportantDeadline, ImportantDeadlineModel>(i)).ToList();
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    Items = _importantDeadline,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<ImportantDeadlineModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
