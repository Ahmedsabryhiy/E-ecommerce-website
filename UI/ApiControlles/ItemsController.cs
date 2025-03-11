using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.ModelViews;

namespace UI.ApiControllers
{
    /// <summary>
    /// API Controller for managing items in the LapShop.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItems<TbItem> oClsItems;

        /// <summary>
        /// Constructor for initializing the ItemsController.
        /// </summary>
        /// <param name="oItems">Dependency injection for the items service.</param>
        public ItemsController(IItems<TbItem> oItems)
        {
            oClsItems = oItems;
        }

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>An ApiResponse containing the list of items.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsItems.GetAll();
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
        /// Retrieves a specific item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>An ApiResponse containing the item data.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsItems.GetById(id);
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
        /// Adds a new item to the collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>An ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbItem item)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsItems.Save(item);
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
        /// Deletes an item based on its ID.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>An ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbItem item)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsItems.Delete(item.ItemId);
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
