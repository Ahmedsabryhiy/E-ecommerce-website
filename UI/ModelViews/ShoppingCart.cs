namespace UI.ModelViews
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            lstItems = new List<ShoppingCartItem>();
        }
        public List<ShoppingCartItem> lstItems { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; } = 0;
        public string PromoCode { get; set; }=string.Empty;
    }
}
