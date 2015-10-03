using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public class UserDao : IUserDao, IDisposable
    {
        TwitterEntities context;

        public UserDao()
        {
            context = new TwitterEntities();
        }

        public ICollection<User> GetList()
        {
            ICollection<User> result;
            result = context.Users.ToList();
            return result;
        }

        public bool Add(User user)
        {
            bool result = false;
            context.Users.Add(user);
            result = context.SaveChanges() > 0;
            return result;
        }

        public bool Delete(User user)
        {
            bool result = false;
            context.Users.Remove(user);
            result = context.SaveChanges() > 0;
            return result;
        }

        public User GetByUsername(string username)
        {
            User result = null;
            result = context.Users.FirstOrDefault(x => x.Username == username);
            return result;
        }

        public User Get(int id)
        {
            User result = null;
            result = context.Users.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
