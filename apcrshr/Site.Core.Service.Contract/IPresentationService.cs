using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Core.Service.Contract
{
    public interface IPresentationService
    {
        /// <summary>
        /// Find Presentation by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FindItemReponse<PresentationModel> FindPresentationByID(string id);

        /// <summary>
        /// Get Presentation detail
        /// </summary>
        /// <param name="actionURL"></param>
        /// <returns></returns>
        FindItemReponse<PresentationModel> FindPresentationByActionURL(string actionURL);

        /// <summary>
        /// Delete Presentation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse DeletePresentation(string id);

        /// <summary>
        /// Update Presentation
        /// </summary>
        /// <param name="Presentation"></param>
        /// <returns></returns>
        BaseResponse UpdatePresentation(PresentationModel presentation);

        /// <summary>
        /// Get all Presentation
        /// </summary>
        /// <returns></returns>
        FindAllItemReponse<PresentationModel> GetPresentation();

        /// <summary>
        /// Get Presentation with paging
        /// </summary>
        /// <param name="pageSize">int</param>
        /// <param name="pageIndex">int</param>
        /// <returns></returns>
        FindAllItemReponse<PresentationModel> GetPresentation(int pageSize, int pageIndex);

        /// <summary>
        /// Create Presentation
        /// </summary>
        /// <param name="Presentation"></param>
        /// <returns></returns>
        InsertResponse CreatePresentation(PresentationModel presentation);

        /// <summary>
        /// Get Presentation by ID
        /// </summary>
        /// <param name="PresentationID"></param>
        /// <returns></returns>
        FindItemReponse<PresentationModel> GetPresentationByID(string presentationID);

        /// <summary>
        /// Get all related Presentation by date
        /// </summary>
        /// <param name="category"></param>
        /// <param name="date"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        FindAllItemReponse<PresentationModel> GetRelatedPresentation(DateTime date, int pageSize, int pageIndex);
    }
}
