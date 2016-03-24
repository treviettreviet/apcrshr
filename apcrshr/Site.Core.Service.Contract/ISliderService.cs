using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface ISliderService
    {
        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<SliderModel> FindSliderByID(string id);
        /// <summary>
        /// Find by Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        FindItemReponse<SliderModel> FindSliderByURL(string url);
        /// <summary>
        /// Update Slider
        /// </summary>
        /// <param name="Slider"></param>
        /// <returns></returns>
        BaseResponse UpdateSlider(SliderModel slider);
        /// <summary>
        /// Get all Slider
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<SliderModel> GetSliders();
        /// <summary>
        /// Get all Slider pagging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        FindAllItemReponse<SliderModel> GetSliders(int pageSize, int pageIndex);
        /// <summary>
        /// Create Slider
        /// </summary>
        /// <param name="Slider"></param>
        /// <returns></returns>
        InsertResponse CreateSlider(SliderModel slider);
        /// <summary>
        /// Delete Slider
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeleteSlider(string id);
        /// <summary>
        /// Find Slider by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        FindItemReponse<SliderModel> GetSliderByTitle(string title);
        /// <summary>
        /// Get all Slider top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        FindAllItemReponse<SliderModel> GetTopSlider(int top, string language);
        /// <summary>
        /// Get all related Slider by date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        FindAllItemReponse<SliderModel> GetRelatedSlider(DateTime date, int pageSize, int pageIndex);
    }
}
