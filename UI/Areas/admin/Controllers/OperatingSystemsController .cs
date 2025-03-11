using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using UI.Utlities;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller for managing operating systems in the admin area.
    /// </summary>
    [Area("admin")]
    public class OperatingSystemsController : Controller
    {
        private readonly IOS<TbO> oClsOperatingSystems;

        /// <summary>
        /// Constructor to initialize the operating systems service.
        /// </summary>
        /// <param name="oOperatingSystems">Operating systems service.</param>
        public OperatingSystemsController(IOS<TbO> oOperatingSystems)
        {
            this.oClsOperatingSystems = oOperatingSystems;
        }

        /// <summary>
        /// Displays a list of operating systems.
        /// </summary>
        /// <returns>View with a list of operating systems.</returns>
        public IActionResult List()
        {
            var lstOperatingSystems = oClsOperatingSystems.GetAll();
            return View(lstOperatingSystems);
        }

        /// <summary>
        /// Displays the form for editing an operating system. If operatingSystemId is null, a new operating system form is displayed.
        /// </summary>
        /// <param name="operatingSystemId">ID of the operating system to be edited (optional).</param>
        /// <returns>View with the operating system details for editing.</returns>
        public IActionResult Edit(int? operatingSystemId)
        {
            var operatingSystem = new TbO();

            if (operatingSystemId != null)
            {
                operatingSystem = oClsOperatingSystems.GetById(Convert.ToInt32(operatingSystemId));
            }
            return View(operatingSystem);
        }

        /// <summary>
        /// Saves the operating system details to the database.
        /// </summary>
        /// <param name="operatingSystem">Operating system data to save.</param>
        /// <param name="Files">Uploaded images associated with the operating system.</param>
        /// <returns>Redirects to the list of operating systems if successful, otherwise returns to the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbO operatingSystem, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", operatingSystem);
            operatingSystem.ImageName = await Helper.UploadImage(Files, "OperatingSystems");
            oClsOperatingSystems.Save(operatingSystem);
            return RedirectToAction("List", operatingSystem);
        }

        /// <summary>
        /// Deletes an operating system based on the provided operatingSystemId.
        /// </summary>
        /// <param name="operatingSystemId">ID of the operating system to delete.</param>
        /// <returns>Redirects to the list of operating systems after deletion.</returns>
        public IActionResult Delete(int operatingSystemId)
        {
            oClsOperatingSystems.Delete(operatingSystemId);
            return RedirectToAction("List");
        }
    }
}
