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
    public class SliderService : ISliderService
    {
        public DataModel.Response.FindItemReponse<DataModel.Model.SliderModel> FindSliderByID(string id)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                Slider slider = sliderRepository.FindByID(id);
                var _slider = MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(slider);
                return new FindItemReponse<SliderModel>
                {
                    Item = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindItemReponse<DataModel.Model.SliderModel> FindSliderByURL(string url)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                Slider slider = sliderRepository.FindByActionURL(url);
                var _slider = MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(slider);
                return new FindItemReponse<SliderModel>
                {
                    Item = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.BaseResponse UpdateSlider(DataModel.Model.SliderModel slider)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                Slider _slider = MapperUtil.CreateMapper().Mapper.Map<SliderModel, Slider>(slider);
                sliderRepository.Update(_slider);
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

        public DataModel.Response.FindAllItemReponse<DataModel.Model.SliderModel> GetSliders()
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                IList<Slider> slider = sliderRepository.FindAll();
                var _slider = slider.Select(i => MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(i)).ToList();
                return new FindAllItemReponse<SliderModel>
                {
                    Items = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.SliderModel> GetSliders(int pageSize, int pageIndex)
        {
            try
            {
                ISliderRepository sliderRespository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                var result = sliderRespository.FindAll(pageSize, pageIndex);
                var _slider = result.Item2.Select(i => MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(i)).ToList();
                return new FindAllItemReponse<SliderModel>
                {
                    Count = result.Item1,
                    Items = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.InsertResponse CreateSlider(DataModel.Model.SliderModel slider)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                Slider _slider = MapperUtil.CreateMapper().Mapper.Map<SliderModel, Slider>(slider);
                object id = sliderRepository.Insert(_slider);
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

        public DataModel.Response.BaseResponse DeleteSlider(string id)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                sliderRepository.Delete(id);
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

        public DataModel.Response.FindItemReponse<DataModel.Model.SliderModel> GetSliderByTitle(string title)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                Slider slider = sliderRepository.FindByTitle(title);
                var _slider = MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(slider);
                return new FindItemReponse<SliderModel>
                {
                    Item = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.SliderModel> GetTopSlider(int top)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();
                IList<Slider> result = sliderRepository.FindTop(top);
                var _slider = result.Select(i => MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(i)).ToList();
                return new FindAllItemReponse<SliderModel>
                {
                    Items = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }

        public DataModel.Response.FindAllItemReponse<DataModel.Model.SliderModel> GetRelatedSlider(DateTime date, int pageSize, int pageIndex)
        {
            try
            {
                ISliderRepository sliderRepository = RepositoryClassFactory.GetInstance().GetSliderRepository();

                var result = sliderRepository.FindAllRelated(date, pageSize, pageIndex);
                var _slider = result.Item2.Select(n => MapperUtil.CreateMapper().Mapper.Map<Slider, SliderModel>(n)).ToList();
                return new FindAllItemReponse<SliderModel>
                {
                    Count = result.Item1,
                    Items = _slider,
                    ErrorCode = (int)ErrorCode.None,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new FindAllItemReponse<SliderModel>
                {
                    ErrorCode = (int)ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
    }
}
