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
    public class LeaderShipService : ILeaderShipService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.LeaderShipModel> FindByID(string id)
        {
            try
            {
                ILeaderShipRepository leaderShipRepository = RepositoryClassFactory.GetInstance().GetLeaderShipRepository();
                LeaderShip leadership = leaderShipRepository.FindByID(id);
                var _leadership = MapperUtil.CreateMapper().Mapper.Map<LeaderShip, LeaderShipModel>(leadership);
                return new FindItemReponse<LeaderShipModel>
                {
                    Item = _leadership,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };


            }
            catch (Exception ex)
            {

                return new FindItemReponse<LeaderShipModel>
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
                ILeaderShipRepository leaderShipRepository = RepositoryClassFactory.GetInstance().GetLeaderShipRepository();
                leaderShipRepository.Delete(id);
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

        public DataModel.Response.InsertResponse Create(DataModel.Model.LeaderShipModel leadership)
        {
            try
            {
                ILeaderShipRepository leaderShipRepository = RepositoryClassFactory.GetInstance().GetLeaderShipRepository();
                IList<LeaderShip> _leaderships = leaderShipRepository.FindByOrganization(leadership.Organization);
                if (_leaderships != null && _leaderships.Count > 0)
                {
                    return new InsertResponse
                    {
                        ErrorCode = (int)ErrorCode.Error,
                        Message = string.Format(Resources.Resource.msg_insert_exists, "LeaderShip", leadership.Organization)
                    };
                }
                var _leadership = MapperUtil.CreateMapper().Mapper.Map<LeaderShipModel, LeaderShip>(leadership);
                object id = leaderShipRepository.Insert(_leadership);
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

        public DataModel.Response.BaseResponse Update(DataModel.Model.LeaderShipModel leadership)
        {
            try
            {
                ILeaderShipRepository leaderShipRepository = RepositoryClassFactory.GetInstance().GetLeaderShipRepository();
                var _leadership = MapperUtil.CreateMapper().Mapper.Map<LeaderShipModel, LeaderShip>(leadership);
                leaderShipRepository.Update(_leadership);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.LeaderShipModel> GetAlls()
        {
            try
            {
                ILeaderShipRepository leaderShipRepository = RepositoryClassFactory.GetInstance().GetLeaderShipRepository();
                IList<LeaderShip> leaderships = leaderShipRepository.FindAll();
                var _leaderships = leaderships.Select(n => MapperUtil.CreateMapper().Mapper.Map<LeaderShip, LeaderShipModel>(n)).ToList();
                return new FindAllItemReponse<LeaderShipModel>
                {
                    Items = _leaderships,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<LeaderShipModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }


        public FindAllItemReponse<LeaderShipModel> FindByscholarshipID(string scholarship)
        {
            try
            {
                ILeaderShipRepository leaderShipRepository = RepositoryClassFactory.GetInstance().GetLeaderShipRepository();
                IList<LeaderShip> leaderships = leaderShipRepository.FindByYouthScholarshipID(scholarship);
                var _leaderships = leaderships.Select(n => MapperUtil.CreateMapper().Mapper.Map<LeaderShip, LeaderShipModel>(n)).ToList();
                return new FindAllItemReponse<LeaderShipModel>
                {
                    Items = _leaderships,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {

                return new FindAllItemReponse<LeaderShipModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
