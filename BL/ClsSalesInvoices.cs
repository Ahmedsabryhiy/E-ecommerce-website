using LapShop.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  BL
{
    public interface ISalesInvoices
    {
        public List<VwSalesInvoice> GetAll();
        public TbSalesInvoice  GetById(int? id);
        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew);
        public bool Delete(int id);
    }
    public  class ClsSalesInvoices:ISalesInvoices
    {
        LapShopContext context;
        ISalesInvoiceItems<TbSalesInvoiceItem> oClsSalesInvoiceItems;
        public ClsSalesInvoices(LapShopContext context, 
            ISalesInvoiceItems<TbSalesInvoiceItem> oSalesInvoiceItems)
        {
            this.context = context;
            this.oClsSalesInvoiceItems = oSalesInvoiceItems;
        }
        public List<VwSalesInvoice> GetAll()
        {
            try
            {

                var lstItems = context.VwSalesInvoices.ToList();
                return lstItems;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public TbSalesInvoice GetById(int? id)
        {
            try
            {
                var item = context.TbSalesInvoices.FirstOrDefault
              (a => a.InvoiceId == id && a.CurrentState == 1);
                if (item == null)
                    return new TbSalesInvoice();
                else
                    return item;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public bool Save (TbSalesInvoice item,List<TbSalesInvoiceItem > lstItems,bool isNew)
        {
            using var transaction=context .Database.BeginTransaction();
            try
            {
                item.CurrentState = 1;
                if (isNew)
                {
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                    context.TbSalesInvoices.Add(item);
                }

                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                    context.Entry(item).State = EntityState.Modified;
                }

                context.SaveChanges();
                oClsSalesInvoiceItems.Save(lstItems, item.InvoiceId, true);
                transaction.Commit();
                return true;
            }
            catch(Exception ex) 
            {
                transaction.Rollback();
                throw new Exception();
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var saleInvoice = GetById(id);
                if (saleInvoice == null) 
                    return false;

                saleInvoice.CurrentState = 0;
                context.Entry(saleInvoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
         

        }
    }
}
