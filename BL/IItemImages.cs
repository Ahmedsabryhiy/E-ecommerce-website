
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  interface IItemImages< T> 
    {
        public List<T> GetAll();
        public T GetById(int? id);
        public List<T> GetByItemImageId(int id);
        public bool Save(T category);
        public bool Delete(int id);
    }
}
