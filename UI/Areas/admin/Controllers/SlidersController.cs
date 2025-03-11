using BL;
using LapShop.Domain.Entities;

using UI.Utlities;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.admin.Controllers
{
    /// <summary>
    /// Controller for managing sliders in the admin area.
    /// </summary>
    [Area("admin")]
    public class SlidersController : Controller
    {
        private readonly ISliders<TbSlider> oClsSliders;

        /// <summary>
        /// Constructor to initialize the sliders service.
        /// </summary>
        /// <param name="oSliders">Sliders service.</param>
        public SlidersController(ISliders<TbSlider> oSliders)
        {
            this.oClsSliders = oSliders;
        }

        /// <summary>
        /// Displays a list of sliders.
        /// </summary>
        /// <returns>View with a list of sliders.</returns>
        public IActionResult List()
        {
            var lstSliders = oClsSliders.GetAll();
            return View(lstSliders);
        }

        /// <summary>
        /// Displays the form for editing a slider based on the provided sliderId.
        /// </summary>
        /// <param name="sliderId">ID of the slider to be edited.</param>
        /// <returns>View with the slider details for editing.</returns>
        public IActionResult Edit(int? sliderId)
        {
            var slider = new TbSlider();

            if (sliderId != null)
            {
                slider = oClsSliders.GetById(Convert.ToInt32(sliderId));
            }
            return View(slider);
        }

        /// <summary>
        /// Saves the slider details to the database.
        /// </summary>
        /// <param name="slider">Slider data to save.</param>
        /// <param name="Files">Uploaded files associated with the slider.</param>
        /// <returns>Redirects to the list of sliders if successful, otherwise returns to the edit view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbSlider slider, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", slider);
            slider.ImageName = await Helper.UploadImage(Files, "Sliders");
            oClsSliders.Save(slider);
            return RedirectToAction("List", slider);
        }

        /// <summary>
        /// Deletes a slider based on the provided sliderId.
        /// </summary>
        /// <param name="sliderId">ID of the slider to delete.</param>
        /// <returns>Redirects to the list of sliders.</returns>
        public IActionResult Delete(int sliderId)
        {
            oClsSliders.Delete(sliderId);
            return RedirectToAction("List");
        }
    }
}
