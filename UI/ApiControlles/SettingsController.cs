using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using Microsoft.AspNetCore.Mvc;
using UI.ModelViews;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.ApiControlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettings<TbSetting> oClsSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsController"/> class.
        /// </summary>
        /// <param name="oSettings">Service for managing settings.</param>
        public SettingsController(ISettings<TbSetting> oSettings)
        {
            this.oClsSettings = oSettings;
        }

        /// <summary>
        /// Retrieves all settings.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing all settings.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Fetch all settings and return with a status code of 200
                oApiResponse.Data = oClsSettings.GetAll();
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 404 status code
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "404";
            }

            return oApiResponse;
        }

        /// <summary>
        /// Saves a new setting.
        /// </summary>
        /// <param name="setting">The setting object to save.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbSetting setting)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Save the new setting and return a status code of 200
                oClsSettings.Save(setting);

                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a 404 status code
                oApiResponse.Data = "null";
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "404";
            }

            return oApiResponse;
        }
    }
}
