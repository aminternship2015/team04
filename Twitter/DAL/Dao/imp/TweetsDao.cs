using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public class TweetsDao : ITweetsDao
    {
        TwitterEntities context;
        IUserDao userDao;

        public TweetsDao(IUserDao userContext)
        {
            this.userDao = userContext;
        }

        public ICollection<Tweet> GetList()
        {
            ICollection<Tweet> result;
            using (context = new TwitterEntities())
            {
                result = context.Tweets.ToList();
            }
            return result;
        }

        public bool Add(Tweet tweet)
        {
            bool result = false;
            using (context = new TwitterEntities())
            {
                //context.Tweets.Attach(tweet);
                context.Tweets.Add(tweet);

                result = context.SaveChanges() > 0;
            }
            return result;
        }

        public bool Delete(Tweet tweet)
        {
            bool result = false;
            using (context = new TwitterEntities())
            {
                context.Tweets.Remove(tweet);
                result = context.SaveChanges() > 0;
            }
            return result;
        }
    }
}
