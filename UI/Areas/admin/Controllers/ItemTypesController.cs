using BL;

using UI.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LapShop.Domain.Entities;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller for managing item types in the admin area.
    /// </summary>
    [Area("admin")]
    public class ItemTypesController : Controller
    {
        private readonly IItemTypes<TbItemType> oClsItemTypes;

        /// <summary>
        /// Constructor to initialize the item types service.
        /// </summary>
        /// <param name="oItemTypes">Item types service.</param>
        public ItemTypesController(IItemTypes<TbItemType> oItemTypes)
        {
            oClsItemTypes = oItemTypes;
        }

        /// <summary>
        /// Displays a list of item types.
        /// </summary>
        /// <returns>View with a list of item types.</returns>
        public IActionResult List()
        {
            var lstItemTypes = oClsItemTypes.GetAll();
            return View(lstItemTypes);
        }

        /// <summary>
        /// Displays the form for editing an item type. If itemTypeId is null, a new item type form is displayed.
        /// </summary>
        /// <param name="itemTypeId">ID of the item type to be edited (optional).</param>
        /// <returns>View with the item type details for editing.</returns>
        public IActionResult Edit(int? itemTypeId)
        {
            var itemType = new TbItemType();

            if (itemTypeId != null)
            {
                itemType = oClsItemTypes.GetById(Convert.ToInt32(itemTypeId));
            }
            return View(itemType);
        }

        /// <summary>
        /// Saves the item type details to the database.
        /// </summary>
        /// <param name="itemType">Item type data to save.</param>
        /// <param name="Files">Uploaded images associated with the item type.</param>
        /// <returns>Redirects to the list of item types if successful, otherwise returns to the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItemType itemType, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", itemType);
            itemType.ImageName = await Helper.UploadImage(Files, "ItemTypes");
            oClsItemTypes.Save(itemType);
            return RedirectToAction("List", itemType);
        }

        /// <summary>
        /// Deletes an item type based on the provided itemTypeId.
        /// </summary>
        /// <param name="itemTypeId">ID of the item type to delete.</param>
        /// <returns>Redirects to the list of item types after deletion.</returns>
        public IActionResult Delete(int itemTypeId)
        {
            oClsItemTypes.Delete(itemTypeId);
            return RedirectToAction("List");
        }
    }
}
