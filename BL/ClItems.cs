using LapShop.Domain.Entities;
using UI.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ClItems : IItems<TbItem>
    {

        LapShopContext context;
        public ClItems(LapShopContext context)
        {
            this.context = context;
        }

        // E-ecommerce website

        public List<TbItem> GetAll()
        {
            try
            {
                var lstItems = context.TbItems.Where(a => a.CurrentState == 1).ToList();
                return lstItems;
            }
            catch
            {
                return new List<TbItem>();
            }

        }
        public List<VwItem> GetAllItemsData(int? categoryId,int? itemTypeId)
        {
            try
            {

                var lstItems = context.VwItems.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0)
               && (a.CategoryId == itemTypeId || itemTypeId == null || itemTypeId == 0) && a.CurrentState == 1 && !string.IsNullOrEmpty(a.ItemName))
                    .OrderByDescending(a => a.CreatedDate).ToList();
                return lstItems;

              }
            catch
            {
                return new List<VwItem>();
            }
        }
        public List<VwItem> GetRecommendedItems(int itemId)
        {
            try
            {
                var item=GetById(itemId);
                var lstRecommededItems=context.VwItems.Where (a=>a.SalesPrice>item.SalesPrice-150&&
                a.SalesPrice < item.SalesPrice + 150 && a.CategoryId==item.CategoryId).ToList();
                return lstRecommededItems;
            }
            catch
            {
                return new List<VwItem>();
            }
        }
        public List<VwItem> GetItemByCategoryId(int categoryId)
        {
            try
            {
              var lstItem=context.VwItems.Where(a=>a.CategoryId==categoryId).ToList();
                return lstItem;
            }
            catch
            {
                return new List<VwItem>();
            }
        }

        public TbItem GetById(int id)
        {
            try
            {
                var item = context.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;

            }
            catch
            {
                return new TbItem();
            }

        }
       
        public VwItem GetItemById(int id)
        {
            try
            {
                var item = context.VwItems.Where(a => a.CurrentState == 1).FirstOrDefault();

                return item;

            }
            catch
            {
                return new VwItem();
            }
        }
        public bool Save(TbItem item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                    item.CurrentState = 1;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = "Ahmed";
                    context.TbItems.Add(item);
                    context.SaveChanges();

                }
                else
                {
                    item.CurrentState = 1;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = "Ahmed";
                    item.UpdatedDate = DateTime.Now;
                    context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var item = GetById(id);
               item.CurrentState = 0;
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
