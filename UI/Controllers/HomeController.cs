using BL;
using   UI.ModelViews;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LapShop.Domain.Entities;
using UI;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        IItems<TbItem> oClsItems;
        ISliders<TbSlider > oClsSliders;
        ICategores<TbCategory > oClsCategories;
        public HomeController(IItems<TbItem> oItems, ICategores<TbCategory> oCategories,
           ISliders<TbSlider> oSliders  )
        {
            oClsItems = oItems;
            oClsCategories = oCategories;
            oClsSliders = oSliders;
        }
        public IActionResult Index()
        {
            

            VmHomePage vm = new VmHomePage();
            vm.lstAllItems = oClsItems.GetAllItemsData(null, null).Take(12).ToList();
            vm .lstNewItems= oClsItems.GetAllItemsData(null,null).Skip(100).Take(20).ToList();
            vm.lstFreeDelivryItems= oClsItems.GetAllItemsData(null,null ).Skip(200).Take(4).ToList();
            vm.lstCategories = oClsCategories.GetAll();
            vm.lstRecommendedItems=oClsItems.GetAllItemsData(null ,null ).Skip(300).Take(4).ToList();
            vm.lstSliders = oClsSliders.GetAll();
            return View(vm);
        }
    }
}
