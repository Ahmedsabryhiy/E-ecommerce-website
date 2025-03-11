using BL;
using LapShop.Domain.Entities;

using UI.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using UI.ModelViews;

namespace LapShop.Controllers
{
    public class OrdersController : Controller
    {
        #region hashCode
        //IItems<TbItem> oClsItems;
        //public OrdersController(IItems<TbItem>oItems)
        //{
        //    oClsItems = oItems;
        //}
        //public IActionResult Cart()
        //{ 
        //    string sessionCart=string.Empty;
        //    if (HttpContext.Session.GetString("Cart") != null)
        //        sessionCart = HttpContext.Session.GetString("Cart");
        //  var cart= JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
        //    return View(cart );
        //}
        //public IActionResult AddToCart(int itemId)
        //{
        //    ShoppingCart cart;
        //    var item =oClsItems.GetById(itemId );
        //    if (HttpContext.Session.GetString("Cart") != null) 
        //     cart=JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString("Cart"));
        //   else
        //        cart = new ShoppingCart();
        //    var Item =oClsItems.GetById(itemId);
        //    var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();
        //  if (itemInList != null)
        //    {
        //        itemInList.Qty++;
        //        itemInList.Total=itemInList .Qty*itemInList.Price;
        //    }
        //    else
        //    {
        //        cart.lstItems.Add(new ShoppingCartItem
        //        {
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            ItemImage = item.ImageName,
        //            Qty = 1,
        //            Price = item.SalesPrice,
        //            Total = item.SalesPrice,

        //        });
        //    }
        //  cart .TotalPrice =cart .lstItems .Sum (a => a.Total );

        //    HttpContext.Session .SetString("Cart",JsonConvert.SerializeObject(cart));
        //    return RedirectToAction("Cart");
        //}

        #endregion

        IItems<TbItem> oClsItems;
        ISalesInvoices oClsSalesInvoices;
        UserManager<ApplicationUsers> oClsUserManager;
        public OrdersController(IItems<TbItem> oItems,
            ISalesInvoices oSalesInvoices
            , UserManager<ApplicationUsers> oUserManager)
        {
            oClsItems = oItems;
            this.oClsSalesInvoices = oSalesInvoices;
            oClsUserManager = oUserManager;
        }
        public IActionResult Cart()
        {
            string sesstionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            return View(cart);
        }
        
        public IActionResult MyOrders()
        {
            return View();
        }

        //[Authorize]
        //public async Task<IActionResult> OrderSuccess()
        //{
        //    string sesstionCart = string.Empty;
        //    if (HttpContext.Request.Cookies["Cart"] != null)
        //        sesstionCart = HttpContext.Request.Cookies["Cart"];
        //    var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);

        //    return View();
        //}
        [Authorize]
        public async Task<  IActionResult> OrdersSuccess()
        {
            string sesstionCart=string .Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sesstionCart = HttpContext.Request.Cookies["Cart"];
            var cart =JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
           await    SaveOrder(cart);
            return View(cart);
          
        }

        public IActionResult AddToCart(int itemId)
        {
            ShoppingCart cart;

            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new ShoppingCart();

            var item = oClsItems.GetById(itemId);

            var itemInList = cart.lstItems.Where(a => a.ItemId == itemId).FirstOrDefault();

            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                cart.lstItems.Add(new ShoppingCartItem
                {
                    ItemId = item.ItemId,
                    ItemsName = item.ItemName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice
                });
            }
            cart.TotalPrice = cart.lstItems.Sum(a => a.Total);

            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Cart");
        }
         async Task SaveOrder(ShoppingCart oShoppingCart)
         {
            try
            {
                List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
                foreach (var item in oShoppingCart.lstItems )
                {
                   lstInvoiceItems .Add ( new TbSalesInvoiceItem ()
                   {
                       ItemId = item.ItemId,
                       Qty = item.Qty,
                       InvoicePrice = item.Price
                   });
                }
                var user=await oClsUserManager.GetUserAsync(User);
                TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
                {
                    InvoiceDate = DateTime.Now,
                    CustomerId = Guid.Parse(user.Id),
                    DelivryDate = DateTime.Now.AddDays(5),
                    CreatedBy = user.Id,
                    CreatedDate = DateTime.Now
                };

                oClsSalesInvoices.Save(oSalesInvoice, lstInvoiceItems, true);
            }
            
            catch (Exception ex)
            {
                  
            }
            
        }
       
    }
}
