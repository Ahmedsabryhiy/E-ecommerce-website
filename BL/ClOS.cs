using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  class ClOS:IOS<TbO >
    {
        LapShopContext context;
        public ClOS(LapShopContext context)
        {
            this.context = context;
        }



        public List<TbO> GetAll()
        {
            try
            {
                var lstoses = context.TbOs.Where(a => a.CurrentState == 1).ToList();
                return lstoses;
            }
            catch
            {
                return new List<TbO>();
            }

        }

        public TbO GetById(int? id)
        {
            try
            {
                var os = context.TbOs.Where(a => a.CurrentState == 1).FirstOrDefault();

                return os;

            }
            catch
            {
                return new TbO();
            }

        }

        public bool Save(TbO os)
        {
            try
            {
                if (os.OsId == 0)
                {
                    os.CurrentState = 1;
                    os.CreatedDate = DateTime.Now;
                    os.CreatedBy = "Ahmed";
                    context.TbOs.Add(os);
                    context.SaveChanges();

                }
                else
                {
                    os.CurrentState = 1;
                    os.CreatedDate = DateTime.Now;
                    os.CreatedBy = "Ahmed";
                    os.UpdatedDate = DateTime.Now;
                    context.Entry(os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var os = GetById(id);
                os.CurrentState = 0;
                    context.Entry(os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
