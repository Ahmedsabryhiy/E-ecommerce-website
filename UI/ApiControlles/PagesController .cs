using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using Microsoft.AspNetCore.Mvc;
using UI.ModelViews;

namespace UI.ApiControlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly ISliders<TbSlider> oClsSliders;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlidersController"/> class.
        /// </summary>
        /// <param name="oSliders">An instance of the sliders service interface.</param>
        public SlidersController(ISliders<TbSlider> oSliders)
        {
            oClsSliders = oSliders;
        }

        /// <summary>
        /// Retrieves all sliders.
        /// </summary>
        /// <returns>An ApiResponse containing the list of all sliders.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsSliders.GetAll();
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "404";
            }

            return oApiResponse;
        }

        /// <summary>
        /// Retrieves a specific slider by ID.
        /// </summary>
        /// <param name="id">The ID of the slider to retrieve.</param>
        /// <returns>An ApiResponse containing the slider data.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsSliders.GetById(id);
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "404";
            }

            return oApiResponse;
        }

        /// <summary>
        /// Saves a new slider.
        /// </summary>
        /// <param name="Slider">The slider data to save.</param>
        /// <returns>An ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbSlider Slider)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsSliders.Save(Slider);
                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "404";
            }

            return oApiResponse;
        }

        /// <summary>
        /// Deletes a specific slider.
        /// </summary>
        /// <param name="slider">The slider data to delete.</param>
        /// <returns>An ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbSlider slider)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsSliders.Delete(slider.SliderId);
                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "404";
            }

            return oApiResponse;
        }
    }
}
