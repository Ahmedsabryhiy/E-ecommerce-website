using BL;
using LapShop.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class PagesController : Controller
    { 
        IPages<TbPage > oClsPage;
        public PagesController(IPages<TbPage> oPage)
        {
            oClsPage = oPage;
        }

        public IActionResult Index(int PageId)
        {
            var page=oClsPage.GetById(PageId);
            return View(page );
        }
    }
}
