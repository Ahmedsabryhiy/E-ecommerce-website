using LapShop.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IItems<T> 
    {
        public List<T> GetAll();
        //  public List<T> GetItemData();
         public VwItem GetItemById(int id );
        public List<VwItem > GetAllItemsData(int? categoryId,int? itemTypeId);
        public List<VwItem> GetItemByCategoryId(int categoryId);
        public List<VwItem> GetRecommendedItems(int itemId);
        public T GetById(int id);
        public bool Save(T item);
        public bool Delete(int id);
    }
}