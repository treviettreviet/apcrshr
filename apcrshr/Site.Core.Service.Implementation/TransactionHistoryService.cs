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
    public class TransactionHistoryService : ITransactionHistoryService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.TransactionHistoryModel> FindByID(string id)
        {
            try
            {
                ITransactionHistoryRepository transactionRepository = RepositoryClassFactory.GetInstance().GetTransactionHistoryRepository();
                TransactionHistory transaction = transactionRepository.FindByID(id);
                var _transaction = MapperUtil.CreateMapper().Mapper.Map<TransactionHistory, TransactionHistoryModel>(transaction);
                return new FindItemReponse<TransactionHistoryModel>
                {
                    Item = _transaction,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<TransactionHistoryModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse Delete(string id)
        {
            try
            {
                ITransactionHistoryRepository transactionRepository = RepositoryClassFactory.GetInstance().GetTransactionHistoryRepository();
                transactionRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.TransactionHistoryModel transaction)
        {
            try
            {
                ITransactionHistoryRepository transactionRepository = RepositoryClassFactory.GetInstance().GetTransactionHistoryRepository();
                var _transaction = MapperUtil.CreateMapper().Mapper.Map<TransactionHistoryModel, TransactionHistory>(transaction);
                object id = transactionRepository.Insert(_transaction);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.TransactionHistoryModel> GetAlls()
        {
            try
            {
                ITransactionHistoryRepository transactionRepository = RepositoryClassFactory.GetInstance().GetTransactionHistoryRepository();
                IList<TransactionHistory> transactions = transactionRepository.FindAll();
                var _transactions = transactions.Select(n => MapperUtil.CreateMapper().Mapper.Map<TransactionHistory, TransactionHistoryModel>(n)).ToList();
                return new FindAllItemReponse<TransactionHistoryModel>
                {
                    Items = _transactions,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<TransactionHistoryModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.TransactionHistoryModel> FindByUserID(string userID)
        {
            try
            {
                ITransactionHistoryRepository transactionRepository = RepositoryClassFactory.GetInstance().GetTransactionHistoryRepository();
                IList<TransactionHistory> transactions = transactionRepository.FindByUserID(userID);
                var _transactions = transactions.Select(n => MapperUtil.CreateMapper().Mapper.Map<TransactionHistory, TransactionHistoryModel>(n)).ToList();
                return new FindAllItemReponse<TransactionHistoryModel>
                {
                    Items = _transactions,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<TransactionHistoryModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<TransactionHistoryModel> FindByTransactionReference(long referenceId)
        {
            try
            {
                ITransactionHistoryRepository transactionRepository = RepositoryClassFactory.GetInstance().GetTransactionHistoryRepository();
                IList<TransactionHistory> transactions = transactionRepository.FindByTransactionReference(referenceId);
                var _transactions = transactions.Select(n => MapperUtil.CreateMapper().Mapper.Map<TransactionHistory, TransactionHistoryModel>(n)).ToList();
                return new FindAllItemReponse<TransactionHistoryModel>
                {
                    Items = _transactions,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<TransactionHistoryModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
