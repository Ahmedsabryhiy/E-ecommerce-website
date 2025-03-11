using Microsoft.AspNetCore.Mvc;
using BL;
using UI.ModelViews;
using Microsoft.AspNetCore.Authorization;
using LapShop.Domain.Entities;
using UI.ModelViews;

namespace UI.ApiControlles
{
    /// <summary>
    /// API Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatgoriesController : ControllerBase
    {
        private readonly ICategores<TbCategory> oClsCategores;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatgoriesController"/> class.
        /// </summary>
        /// <param name="oCategores">Service for managing categories.</param>
        public CatgoriesController(ICategores<TbCategory> oCategores)
        {
            oClsCategores = oCategores;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A response containing the list of all categories.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsCategores.GetAll();
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = "null";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.StatusCode = 404;
                oApiResponse.Errors = ex.Message;
            }

            return oApiResponse;
        }

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A response containing the requested category.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsCategores.GetById(id);
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = "null";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.StatusCode = 404;
                oApiResponse.Errors = ex.Message;
            }

            return oApiResponse;
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category">The category object to add.</param>
        /// <returns>A response indicating the success of the operation.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbCategory category)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Example: Add the category to the database.
                oApiResponse.Data = "done";
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = "null";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.StatusCode = 404;
                oApiResponse.Errors = ex.Message;
            }

            return oApiResponse;
        }

        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="value">The updated category object.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // Implementation can be added here.
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="item">The item object containing the ID of the category to delete.</param>
        /// <returns>A response indicating the success of the operation.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbItem item)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsCategores.Delete(item.ItemId);
                oApiResponse.Data = "done";
                oApiResponse.StatusCode = 200;
                oApiResponse.Errors = "null";
            }
            catch (Exception ex)
            {
                oApiResponse.Data = "null";
                oApiResponse.StatusCode = 404;
                oApiResponse.Errors = ex.Message;
            }

            return oApiResponse;
        }
    }
}
