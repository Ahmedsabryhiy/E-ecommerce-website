using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public  interface IOS< T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int? id);
        public bool Save(T category);
        public bool Delete(int id);
    }
}
