using LapShop.Domain.Entities;

using  System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class ClItemImages:IItemImages<TbItemImage>
    {
        LapShopContext context;
        public ClItemImages(LapShopContext context)
        {
            this.context = context;
        }



        public List<TbItemImage> GetAll()
        {
            try
            {
                var lstItemTypes = context.TbItemImages.ToList();
                return lstItemTypes;
            }
            catch
            {
                return new List<TbItemImage>();
            }

        }

        public TbItemImage GetById(int? id)
        {
            try
            {
                var itemImage = context.TbItemImages.FirstOrDefault();

                return itemImage;

            }
            catch
            {
                return new TbItemImage();
            }

        }
        public List<TbItemImage> GetByItemImageId(int id)
        {
            try
            {
                var item = context.TbItemImages.Where(a => a.ItemId == id).ToList();
                return item;
            }
            catch
            {
                return new List<TbItemImage>();
            }
        }

        public bool Save(TbItemImage itemImage)
        {
            try
            {
                if (itemImage.ImageId== 0)
                {
                  
                    context.TbItemImages.Add(itemImage);
                    context.SaveChanges();

                }
                else
                {
                  
                    context.Entry(itemImage).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var ItemImage = GetById(id);
               context.TbItemImages.Remove(ItemImage);
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
