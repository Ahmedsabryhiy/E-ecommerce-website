using BL;
using UI.ModelViews;
using Microsoft.AspNetCore.Mvc;
using UI.ModelViews;
using LapShop.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        // Dependency injection of the IPages service
        private readonly IPages<TbPage> oClsPages;

        // Constructor to initialize the PagesController with the IPages service
        public PagesController(IPages<TbPage> oPages)
        {
            oClsPages = oPages;
        }

        /// <summary>
        /// Retrieves all pages.
        /// </summary>
        /// <returns>ApiResponse containing all pages.</returns>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Fetch all pages and return with a status code of 200
                oApiResponse.Data = oClsPages.GetAll();
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
        /// Retrieves a specific page by its ID.
        /// </summary>
        /// <param name="id">The ID of the page.</param>
        /// <returns>ApiResponse containing the requested page.</returns>
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Fetch a page by its ID and return with a status code of 200
                oApiResponse.Data = oClsPages.GetById(id);
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
        /// Creates a new page.
        /// </summary>
        /// <param name="page">The page object to create.</param>
        /// <returns>ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        public ApiResponse Post([FromBody] TbPage page)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Save the page and return a status code of 200
                oClsPages.Save(page);

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
        /// Deletes a page by its ID.
        /// </summary>
        /// <param name="page">The page object to delete (must include PageId).</param>
        /// <returns>ApiResponse indicating the result of the operation.</returns>
        [HttpPost]
        [Route("Delete")]
        public ApiResponse Delete([FromBody] TbPage page)
        {
            ApiResponse oApiResponse = new ApiResponse();
            try
            {
                // Delete the page by its ID and return a status code of 200
                oClsPages.Delete(page.PageId);

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
