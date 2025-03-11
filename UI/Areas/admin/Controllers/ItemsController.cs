using BL;
using UI.ModelViews;
using UI.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using LapShop.Domain.Entities;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller for managing items in the admin area.
    /// </summary>
    [Authorize(Roles = "Admin,Data Entry")]
    [Area("admin")]
    public class ItemsController : Controller
    {
        private readonly IItems<TbItem> oClItems;
        private readonly ICategores<TbCategory> oClCategores;
        private readonly IItemTypes<TbItemType> oClItemTypes;
        private readonly IOS<TbO> oClOs;

        /// <summary>
        /// Constructor to initialize dependencies for items, categories, item types, and operating systems.
        /// </summary>
        /// <param name="oItem">Items service.</param>
        /// <param name="oCategores">Categories service.</param>
        /// <param name="oItemTypes">Item types service.</param>
        /// <param name="os">Operating systems service.</param>
        public ItemsController(IItems<TbItem> oItem, ICategores<TbCategory> oCategores,
            IItemTypes<TbItemType> oItemTypes, IOS<TbO> os)
        {
            oClItems = oItem;
            oClCategores = oCategores;
            oClItemTypes = oItemTypes;
            oClOs = os;
        }

        /// <summary>
        /// Displays a list of items.
        /// </summary>
        /// <returns>View with a list of items.</returns>
        public IActionResult List()
        {
            var lstItems = oClItems.GetAllItemsData(null, null);
            return View(lstItems);
        }

        /// <summary>
        /// Searches for items based on category and item type.
        /// </summary>
        /// <param name="categoryId">Category ID for filtering items.</param>
        /// <param name="itemTypeId">Item Type ID for filtering items.</param>
        /// <returns>View with a list of filtered items.</returns>
        public IActionResult Serch(int categoryId, int itemTypeId)
        {
            ViewBag.lstItemTypes = oClItemTypes.GetAll();
            ViewBag.lstCategories = oClCategores.GetAll();

            var lstItems = oClItems.GetAllItemsData(categoryId, itemTypeId);
            return View(lstItems);
        }

        /// <summary>
        /// Displays the form for editing an item. If itemId is null, a new item form is displayed.
        /// </summary>
        /// <param name="itemId">ID of the item to be edited (optional).</param>
        /// <returns>View with the item details for editing.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? itemId)
        {
            var item = new TbItem();
            ViewBag.lstCategories = oClCategores.GetAll();
            ViewBag.lstItemTypes = oClItemTypes.GetAll();
            ViewBag.lstOs = oClOs.GetAll();
            if (itemId != null)
            {
                item = oClItems.GetById(Convert.ToInt32(itemId));
            }
            return View(item);
        }

        /// <summary>
        /// Saves the item details to the database.
        /// </summary>
        /// <param name="item">Item data to save.</param>
        /// <param name="Files">Uploaded images associated with the item.</param>
        /// <returns>Redirects to the list of items if successful, otherwise returns to the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", item);
            item.ImageName = await Helper.UploadImage(Files, "Items");
            oClItems.Save(item);
            return RedirectToAction("List", item);
        }

        /// <summary>
        /// Deletes an item based on the provided itemId.
        /// </summary>
        /// <param name="itemId">ID of the item to delete.</param>
        /// <returns>Redirects to the list of items after deletion.</returns>
        public IActionResult Delete(int itemId)
        {
            oClItems.Delete(itemId);
            return RedirectToAction("List");
        }
    }
}
