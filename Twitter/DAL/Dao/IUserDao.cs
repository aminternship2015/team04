using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public interface IUserDao : IBaseDao<User>
    {
         bool Update(User user);
         bool Delete(User user);
         bool Add(User user);
    }
}
