using LapShop.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ISalesInvoiceItems<T>
    {
        public List<T> GetSalesInvoiceId(int id);

        public bool Save(IList<T> Items, int salesInvoiceId, bool isNew);
    }
    public class ClsSalesInvoiceItems : ISalesInvoiceItems<TbSalesInvoiceItem>
    {
        LapShopContext context;
        public ClsSalesInvoiceItems(LapShopContext context)
        {
            this.context = context;
        }
        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id)
        {
            try
            {
                var salesInvoiceItem = context.TbSalesInvoiceItems.Where(a => a.InvoiceId == id).ToList();
                if (salesInvoiceItem == null)
                    return new List<TbSalesInvoiceItem>();
                else
                    return salesInvoiceItem;


            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew)
        {
            List<TbSalesInvoiceItem> dbInvoiceItems =
                 GetSalesInvoiceId(Items[0].InvoiceId);

            foreach (var interfaceItems in Items)
            {
                var dbObject = dbInvoiceItems.Where(a => a.InvoiceItemId == interfaceItems.InvoiceItemId).FirstOrDefault();
                if (dbObject != null)
                {
                    context.Entry(dbObject).State = EntityState.Modified;
                }

                else
                {
                    interfaceItems.InvoiceId = salesInvoiceId;
                    context.TbSalesInvoiceItems.Add(interfaceItems);
                }
            }

            foreach (var item in dbInvoiceItems)
            {
                var interfaceObject = Items.Where(a => a.InvoiceItemId == item.InvoiceItemId).FirstOrDefault();
                if (interfaceObject == null)
                    context.TbSalesInvoiceItems.Remove(item);
            }

            context.SaveChanges();
            return true;
        }
    }
    
}
