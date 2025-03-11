using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.ModelViews;

namespace UI.ApiControlles
{
    /// <summary>
    /// Controller for managing item types in the LapShop API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly IItemTypes<TbItemType> oClsItemTypess;

        /// <summary>
        /// Initializes a new instance of the ItemTypesController.
        /// </summary>
        /// <param name="oItemTypess">The item types service.</param>
        public ItemTypesController(IItemTypes<TbItemType> oItemTypess)
        {
            oClsItemTypess = oItemTypess;
        }

        /// <summary>
        /// Retrieves all item types.
        /// </summary>
        /// <returns>An ApiResponse containing all item types or an error message.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsItemTypess.GetAll();
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
        /// Retrieves a specific item type by its ID.
        /// </summary>
        /// <param name="id">The ID of the item type to retrieve.</param>
        /// <returns>An ApiResponse containing the requested item type or an error message.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsItemTypess.GetById(id);
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
        /// Creates a new item type.
        /// </summary>
        /// <param name="itemTypes">The item type to create.</param>
        /// <returns>An ApiResponse indicating success or failure.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbItemType itemTypes)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsItemTypess.Save(itemTypes);
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
        /// Deletes an item type.
        /// </summary>
        /// <param name="itemTypes">The item type to delete.</param>
        /// <returns>An ApiResponse indicating success or failure.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbItemType itemTypes)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Delete the page by its ID and return a status code of 200
                oClsItemTypess.Delete(itemTypes.ItemTypeId);
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