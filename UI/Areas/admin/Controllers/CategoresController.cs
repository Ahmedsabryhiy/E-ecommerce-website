using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using UI.Utlities;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller to manage categories in the admin area.
    /// </summary>
    [Area("admin")]
    public class CategoresController : Controller
    {
        private readonly ICategores<TbCategory> oClCategores;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoresController"/> class.
        /// </summary>
        /// <param name="oCategores">The categories service.</param>
        public CategoresController(ICategores<TbCategory> oCategores)
        {
            oClCategores = oCategores;
        }

        /// <summary>
        /// Displays the list of categories.
        /// </summary>
        /// <returns>A view containing the list of categories.</returns>
        public IActionResult List()
        {
            var lstCategores = oClCategores.GetAll();
            return View(lstCategores);
        }

        /// <summary>
        /// Displays the edit form for a category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to edit. If null, a new category will be created.</param>
        /// <returns>A view containing the category data.</returns>
        public IActionResult Edit(int? categoryId)
        {
            var category = new TbCategory();

            if (categoryId != null)
            {
                category = oClCategores.GetById(Convert.ToInt32(categoryId));
            }
            return View(category);
        }

        /// <summary>
        /// Saves the category to the database.
        /// </summary>
        /// <param name="category">The category to save.</param>
        /// <param name="Files">The list of uploaded files.</param>
        /// <returns>Redirects to the list of categories if successful, otherwise returns the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", category);

            category.ImageName = await Helper.UploadImage(Files, "Categores");
            oClCategores.Save(category);
            return RedirectToAction("List", category);
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to delete.</param>
        /// <returns>Redirects to the list of categories.</returns>
        public IActionResult Delete(int categoryId)
        {
            oClCategores.Delete(categoryId);
            return RedirectToAction("List");
        }
    }
}
