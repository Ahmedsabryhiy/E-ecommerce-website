using BL;
using UI.ModelViews;
using UI.Utlities;
using Microsoft.AspNetCore.Mvc;
using LapShop.Domain.Entities;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller for managing pages in the admin area.
    /// </summary>
    [Area("admin")]
    public class PagesController : Controller
    {
        private readonly IPages<TbPage> oClsPages;

        /// <summary>
        /// Constructor to initialize the pages service.
        /// </summary>
        /// <param name="oPages">Pages service.</param>
        public PagesController(IPages<TbPage> oPages)
        {
            this.oClsPages = oPages;
        }

        /// <summary>
        /// Displays a list of pages.
        /// </summary>
        /// <returns>View with a list of pages.</returns>
        public IActionResult List()
        {
            var lstPages = oClsPages.GetAll();
            return View(lstPages);
        }

        /// <summary>
        /// Displays the form for editing a page. If pageId is null, a new page form is displayed.
        /// </summary>
        /// <param name="pageId">ID of the page to be edited (optional).</param>
        /// <returns>View with the page details for editing.</returns>
        public IActionResult Edit(int? pageId)
        {
            var page = new TbPage();

            if (pageId != null)
            {
                page = oClsPages.GetById(Convert.ToInt32(pageId));
            }
            return View(page);
        }

        /// <summary>
        /// Saves the page details to the database.
        /// </summary>
        /// <param name="page">Page data to save.</param>
        /// <param name="Files">Uploaded images associated with the page.</param>
        /// <returns>Redirects to the list of pages if successful, otherwise returns to the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbPage page, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", page);
            page.ImageName = await Helper.UploadImage(Files, "Pages");
            oClsPages.Save(page);
            return RedirectToAction("List", page);
        }

        /// <summary>
        /// Deletes a page based on the provided pageId.
        /// </summary>
        /// <param name="pageId">ID of the page to delete.</param>
        /// <returns>Redirects to the list of pages after deletion.</returns>
        public IActionResult Delete(int pageId)
        {
            oClsPages.Delete(pageId);
            return RedirectToAction("List");
        }
    }
}
