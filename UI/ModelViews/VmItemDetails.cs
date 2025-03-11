using LapShop.Domain.Entities;

namespace  UI.ModelViews
{
    public class VmItemDetails
    {
        public VmItemDetails()
        {
            Item =new VwItem ();
            lstRecommndedItems = new List<VwItem>();
            lstItemImages=new List<TbItemImage>();
            lstItemsByCategory=new List<VwItem> ();
        }
        public VwItem  Item { get; set; }
        public List <VwItem> lstRecommndedItems {  get; set; }
        public List<TbItemImage> lstItemImages { get; set; }
        public List<VwItem > lstItemsByCategory { get; set; }
    }
}
