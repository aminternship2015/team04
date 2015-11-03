using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ITweetsDao : IBaseDao<Tweet>
    {
         bool Save(int id, string text);
         bool Update(Tweet tweet);
         bool Delete(Tweet tweet);
         bool Add(Tweet tweet);
    }
}
