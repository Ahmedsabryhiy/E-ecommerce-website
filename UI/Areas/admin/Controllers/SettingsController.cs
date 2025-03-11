using BL;
using LapShop.Domain.Entities;
using UI.ModelViews;
using UI.Utlities;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller for managing settings in the admin area.
    /// </summary>
    [Area("admin")]
    public class SettingsController : Controller
    {
        private readonly ISettings<TbSetting> oClsSettings;

        /// <summary>
        /// Constructor to initialize the settings service.
        /// </summary>
        /// <param name="oSettings">Settings service.</param>
        public SettingsController(ISettings<TbSetting> oSettings)
        {
            oClsSettings = oSettings;
        }

        /// <summary>
        /// Displays a list of settings.
        /// </summary>
        /// <returns>View with a list of settings.</returns>
        public IActionResult List()
        {
            var lstSettings = oClsSettings.GetAll();
            return View(lstSettings);
        }

        /// <summary>
        /// Displays the form for editing a setting based on the provided settingId.
        /// </summary>
        /// <param name="settingId">ID of the setting to be edited.</param>
        /// <returns>View with the setting details for editing.</returns>
        public IActionResult Edit(int settingId)
        {
            var setting = new TbSetting();
            setting = oClsSettings.GetById(settingId);

            return View(setting);
        }

        /// <summary>
        /// Saves the settings details to the database.
        /// </summary>
        /// <param name="setting">Settings data to save.</param>
        /// <param name="Files">Uploaded files associated with the setting.</param>
        /// <returns>Redirects to the list of settings if successful, otherwise returns to the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbSetting setting, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", setting);

            oClsSettings.Save(setting);
            return RedirectToAction("List", setting);
        }
    }
}
