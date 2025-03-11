using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IPages<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int? id);
        public bool Save(T Page);
        public bool Delete(int id);
    }
    public class ClsPages:IPages<TbPage>
    {
        LapShopContext context;
        public ClsPages(LapShopContext context)
        {
            this.context = context;
        }

        public List<TbPage> GetAll()
        {
            try
            {
                var lstPages = context.TbPages.ToList();
                return lstPages;
            }
            catch
            {
                return new List<TbPage>();
            }

        }

        public TbPage GetById(int? id)
        {
            try
            {
                var page = context.TbPages.Where(a=>a.CurrentState==1).FirstOrDefault();

                return page ;

            }
            catch
            {
                return new TbPage();
            }

        }

        public bool Save(TbPage Page)
        {
            try
            {
                if (Page.PageId == 0)
                {
                  
                    context.TbPages.Add(Page);
                    context.SaveChanges();

                }
                else
                {
                    
                    context.Entry(Page).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var page = GetById(id);

                page.CurrentState = 0;
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
