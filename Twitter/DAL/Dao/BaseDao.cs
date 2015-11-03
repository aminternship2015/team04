using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class BaseDao<T> : IBaseDao<T> where T : class
    {
        public ICollection<T> GetList()
        {
            return null;
        }
         public bool Add(T t)
        {
            return true;
        }
        public bool Delete(T t)
        {
            return true;
        }
    }
}
