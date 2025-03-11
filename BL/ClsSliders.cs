using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ISliders<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int? id);
        public bool Save(T slider);
        public bool Delete(int id);
    }
    public class ClSliders:ISliders<TbSlider>
    {
        LapShopContext context;
        public ClSliders(LapShopContext context)
        {
            this.context = context;
        }

        public List<TbSlider> GetAll()
        {
            try
            {
                var lstCategores = context.TbSliders.ToList();
                return lstCategores;
            }
            catch
            {
                return new List<TbSlider>();
            }

        }

        public TbSlider GetById(int? id)
        {
            try
            {
                var slider = context.TbSliders.FirstOrDefault();

                return slider ;

            }
            catch
            {
                return new TbSlider();
            }

        }

        public bool Save(TbSlider slider)
        {
            try
            {
                if (slider.SliderId == 0)
                {
                  
                    context.TbSliders.Add(slider);
                    context.SaveChanges();

                }
                else
                {
                    
                    context.Entry(slider).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var slider = GetById(id);
                context.TbSliders.Remove(slider);
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
