using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class ClCategores:ICategores<TbCategory>
    {
        LapShopContext context;
        public ClCategores(LapShopContext context)
        {
            this.context = context;
        }

        

        public List<TbCategory> GetAll()
        {
            try
            {
                var lstCategores = context.TbCategories .Where(a=>a.CurrentState==1).ToList();
                return lstCategores;
            }
            catch
            {
                return new List<TbCategory>();
            }
           
        }

        public TbCategory GetById(int? id)
        {
            try
            {
                var category = context.TbCategories.Where(a => a.CurrentState == 1).FirstOrDefault();

                return category;

            }
            catch
            {
                return new TbCategory();
            }
            
        }
        
        public bool Save(TbCategory category)
        {
            try
            {
                if (category.CategoryId == 0)
                {
                    category.CurrentState = 1;
                    category .CreatedDate = DateTime.Now;
                    category.CreatedBy = "Ahmed";
                    context.TbCategories.Add(category);
                    context.SaveChanges();

                }
                else
                {
                    category.CurrentState = 1;
                    category.CreatedDate = DateTime.Now;
                    category.CreatedBy = "Ahmed";
                    category .UpdatedDate = DateTime.Now;
                    context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var category = GetById(id);
                //context.TbCategories.Remove(category);
                category.CurrentState = 0;
                context.Entry(category ).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
