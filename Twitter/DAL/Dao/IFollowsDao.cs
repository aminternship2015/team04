using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IFollowsDao : IBaseDao<Follow>
    {
        bool Add(int id, int id2);
        ICollection<User> GetUserFollows(int currentUserId);
        bool DeleteById(int id);
    }
}
