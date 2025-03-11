using BL;
using LapShop.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using UI.ModelViews;

namespace UI.ApiControlles
{
    /// <summary>
    /// Controller for managing item images in the LapShop API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemImagesController : ControllerBase
    {
        private readonly IItemImages<TbItemImage> oClsItemImages;

        /// <summary>
        /// Initializes a new instance of the ItemImagesController.
        /// </summary>
        /// <param name="oItemImages">The item images service.</param>
        public ItemImagesController(IItemImages<TbItemImage> oItemImages)
        {
            oClsItemImages = oItemImages;
        }

        /// <summary>
        /// Retrieves all item images.
        /// </summary>
        /// <returns>An ApiResponse containing all item images or an error message.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsItemImages.GetAll();
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
        /// Retrieves a specific item image by its ID.
        /// </summary>
        /// <param name="id">The ID of the item image to retrieve.</param>
        /// <returns>An ApiResponse containing the requested item image or an error message.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oApiResponse.Data = oClsItemImages.GetById(id);
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
        /// Creates a new item image.
        /// </summary>
        /// <param name="itemImage">The item image to create.</param>
        /// <returns>An ApiResponse indicating success or failure.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbItemImage itemImage)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsItemImages.Save(itemImage);
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
        /// Deletes an item image.
        /// </summary>
        /// <param name="itemImage">The item image to delete.</param>
        /// <returns>An ApiResponse indicating success or failure.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbItemImage itemImage)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                oClsItemImages.Delete(itemImage.ItemId);
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