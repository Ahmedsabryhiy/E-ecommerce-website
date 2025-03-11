using BL;
using LapShop.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using UI;
using UI.ModelViews;

namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {
        IItems<TbItem> oClsItem;
        IItemImages<TbItemImage> oClsItemImages;
        ICategores<TbCategory> oClsCategory;
        public ItemsController(IItems<TbItem> oItem,IItemImages<TbItemImage> oItemImages, ICategores<TbCategory> oCategory)
        {
            oClsItem = oItem;
            oClsItemImages = oItemImages;
            this.oClsCategory = oCategory;
        }
        public IActionResult ListItems()
        {
            return View();
        }
        public IActionResult ItemDetails( int id)
        {
            var item=oClsItem.GetItemById(id);
            VmItemDetails vm = new VmItemDetails();
            vm.Item =item;
            vm.lstItemImages=oClsItemImages.GetByItemImageId(id);
            vm.lstRecommndedItems=oClsItem.GetRecommendedItems(id);
            return View(vm);
        }
    }
}
