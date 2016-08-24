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
    public class PaymentService : IPaymentService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.PaymentModel> FindByID(string id)
        {
            try
            {
                IPaymentRepository paymentRepository = RepositoryClassFactory.GetInstance().GetPaymentRepository();
                Payment payment = paymentRepository.FindByID(id);
                var _payment = MapperUtil.CreateMapper().Mapper.Map<Payment, PaymentModel>(payment);
                return new FindItemReponse<PaymentModel>
                {
                    Item = _payment,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<PaymentModel>
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
                IPaymentRepository paymentRepository = RepositoryClassFactory.GetInstance().GetPaymentRepository();
                paymentRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.PaymentModel payment)
        {
            try
            {
                IPaymentRepository paymentRepository = RepositoryClassFactory.GetInstance().GetPaymentRepository();
                var _Payment = MapperUtil.CreateMapper().Mapper.Map<PaymentModel, Payment>(payment);
                object id = paymentRepository.Insert(_Payment);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.PaymentModel payment)
        {
            try
            {
                IPaymentRepository paymentRepository = RepositoryClassFactory.GetInstance().GetPaymentRepository();
                var _payment = MapperUtil.CreateMapper().Mapper.Map<PaymentModel, Payment>(payment);
                paymentRepository.Update(_payment);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.PaymentModel> GetAlls()
        {
            try
            {
                IPaymentRepository paymentRepository = RepositoryClassFactory.GetInstance().GetPaymentRepository();
                IList<Payment> payments = paymentRepository.FindAll();
                var _payments = payments.Select(n => MapperUtil.CreateMapper().Mapper.Map<Payment, PaymentModel>(n)).ToList();
                return new FindAllItemReponse<PaymentModel>
                {
                    Items = _payments,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<PaymentModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
