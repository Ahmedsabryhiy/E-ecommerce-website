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
    public class OperatingSystemsController : ControllerBase
    {
        private readonly IOS<TbO> oClsOperatingSystems;

        /// <summary>
        /// Constructor to initialize the OperatingSystemsController with the IOS service.
        /// </summary>
        /// <param name="oOperatingSystems">The service for operating system operations.</param>
        public OperatingSystemsController(IOS<TbO> oOperatingSystems)
        {
            oClsOperatingSystems = oOperatingSystems;
        }

        /// <summary>
        /// Retrieves all operating systems.
        /// </summary>
        /// <returns>ApiResponse containing all operating systems.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Fetch all operating systems and return with a status code of 200
                oApiResponse.Data = oClsOperatingSystems.GetAll();
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
        /// Retrieves a specific operating system by its ID.
        /// </summary>
        /// <param name="id">The ID of the operating system.</param>
        /// <returns>ApiResponse containing the requested operating system.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Fetch an operating system by its ID and return with a status code of 200
                oApiResponse.Data = oClsOperatingSystems.GetById(id);
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
        /// Creates a new operating system.
        /// </summary>
        /// <param name="operatingSystem">The operating system object to create.</param>
        /// <returns>ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbO operatingSystem)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Save the new operating system and return a status code of 200
                oClsOperatingSystems.Save(operatingSystem);

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

        /// <summary>
        /// Deletes an operating system by its ID.
        /// </summary>
        /// <param name="operatingSystem">The operating system object to delete (must include OsId).</param>
        /// <returns>ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbO operatingSystem)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Delete the operating system by its ID and return a status code of 200
                oClsOperatingSystems.Delete(operatingSystem.OsId);

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
