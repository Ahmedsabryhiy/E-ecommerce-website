using BL;

using UI.Utlities;
using Microsoft.AspNetCore.Mvc;
using LapShop.Domain.Entities;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// This controller manages item images in the admin area.
    /// </summary>
    [Area("admin")]
    public class ItemImagesController : Controller
    {
        private readonly IItemImages<TbItemImage> oClsItemImages;

        /// <summary>
        /// Constructor to initialize the item images service.
        /// </summary>
        /// <param name="oItemImages">The item images service.</param>
        public ItemImagesController(IItemImages<TbItemImage> oItemImages)
        {
            this.oClsItemImages = oItemImages;
        }

        /// <summary>
        /// Displays the list of item images.
        /// </summary>
        /// <returns>The list of item images.</returns>
        public IActionResult List()
        {
            var lstItemImages = oClsItemImages.GetAll();
            return View(lstItemImages);
        }

        /// <summary>
        /// Displays the edit form for a specific item image.
        /// </summary>
        /// <param name="itemImageId">The ID of the item image to edit. If null, creates a new item image.</param>
        /// <returns>The edit form view.</returns>
        public IActionResult Edit(int? itemImageId)
        {
            var itemImage = new TbItemImage();

            if (itemImageId != null)
            {
                itemImage = oClsItemImages.GetById(Convert.ToInt32(itemImageId));
            }
            return View(itemImage);
        }

        /// <summary>
        /// Saves the item image to the database.
        /// </summary>
        /// <param name="itemImage">The item image object to save.</param>
        /// <param name="Files">The image files to upload.</param>
        /// <returns>Redirects to the list of item images after saving.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItemImage itemImage, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", itemImage);

            itemImage.ImageName = await Helper.UploadImage(Files, "ItemImages");
            oClsItemImages.Save(itemImage);
            return RedirectToAction("List", itemImage);
        }

        /// <summary>
        /// Deletes a specific item image.
        /// </summary>
        /// <param name="itemImageId">The ID of the item image to delete.</param>
        /// <returns>Redirects to the list of item images after deletion.</returns>
        public IActionResult Delete(int itemImageId)
        {
            oClsItemImages.Delete(itemImageId);
            return RedirectToAction("List");
        }
    }
}
