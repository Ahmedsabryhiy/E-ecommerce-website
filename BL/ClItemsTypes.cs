using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ClItemsTypes : IItemTypes<TbItemType>
    {

        LapShopContext context;
        public ClItemsTypes(LapShopContext context)
        {
            this.context = context;
        }



        public List<TbItemType> GetAll()
        {
            try
            {
                var lstItemTypes = context.TbItemTypes.Where(a => a.CurrentState == 1).ToList();
                return lstItemTypes;
            }
            catch
            {
                return new List<TbItemType>();
            }

        }

        public TbItemType GetById(int? id)
        {
            try
            {
                var itemType = context.TbItemTypes.Where(a => a.CurrentState == 1).FirstOrDefault();

                return itemType;

            }
            catch
            {
                return new TbItemType();
            }

        }

        public bool Save(TbItemType ItemType)
        {
            try
            {
                if (ItemType.ItemTypeId == 0)
                {
                    ItemType.CurrentState = 1;
                    ItemType.CreatedDate = DateTime.Now;
                    ItemType.CreatedBy = "Ahmed";
                    context.TbItemTypes.Add(ItemType);
                    context.SaveChanges();

                }
                else
                {
                    ItemType.CurrentState = 1;
                    ItemType.CreatedDate = DateTime.Now;
                    ItemType.CreatedBy = "Ahmed";
                    ItemType.UpdatedDate = DateTime.Now;
                    context.Entry(ItemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var ItemType = GetById(id);
                ItemType.CurrentState = 0;
                context.Entry(ItemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
  

