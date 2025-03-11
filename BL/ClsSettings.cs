using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ISettings<T> where T : class
    {
        public  T GetAll();
        public T GetById(int? id);
        public bool Save(T setting);

    }
    public class ClsSettings : ISettings<TbSetting>
    {
        LapShopContext context;
        public ClsSettings(LapShopContext context)
        {
            this.context = context;
        }

        public  TbSetting GetAll()
        {
            try
            {
                var categores = context.TbSettings.FirstOrDefault();
                return categores;
            }
            catch
            {
                return new TbSetting();
            }
        }
        public TbSetting GetById(int? id)
        {
            try
            {
                var setting = context.TbSettings.FirstOrDefault();

                return setting;

            }
            catch
            {
                return new TbSetting();
            }

        }

        public bool Save(TbSetting slider)
        {
            try
            {

                context.Entry(slider).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
