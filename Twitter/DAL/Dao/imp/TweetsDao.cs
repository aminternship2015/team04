using DAL.Entities;
using StaticLogger;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
    public class TweetsDao : ITweetsDao
    {
        private TwitterEntities context;
        private IUserDao userDao;

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
                tweet.User = userDao.GetByPublisherId(tweet.User_Id);
                context.Tweets.Attach(tweet);
                context.Tweets.Add(tweet);

                result = context.SaveChanges() > 0;
                Logger.Log.Debug("user ID:" + tweet.User_Id + " " + userDao.GetByPublisherId(tweet.User_Id).Email + " added a new tweet ID:" + tweet.Id);
            }

            return result;
        }

        public bool Delete(Tweet tweet)
        {
            bool result = false;
            using (context = new TwitterEntities())
            {
                tweet.User = userDao.GetByPublisherId(tweet.User_Id);
                context.Tweets.Attach(tweet);
                context.Tweets.Remove(tweet);
                //context.Entry(tweet).State = EntityState.Deleted;
                result = context.SaveChanges() > 0;
                Logger.Log.Debug("user ID:" + tweet.User_Id + " " + userDao.GetByPublisherId(tweet.User_Id).Email + " deleted a tweet ID:" + tweet.Id);
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (context = new TwitterEntities())
            {
                var tweet = GetByPublisherId(id);
                context.Tweets.Remove(tweet);
                result = context.SaveChanges() > 0;
                Logger.Log.Debug("user ID:" + tweet.User_Id + " " + userDao.GetByPublisherId(tweet.User_Id).Email + " deleted a tweet ID:" + tweet.Id);
            }

            return result;
        }

        public bool Update(Tweet tweet)
        {
            bool result = false;
            using (context = new TwitterEntities())
            {
                tweet.User = userDao.GetByPublisherId(tweet.User_Id);
                context.Tweets.Attach(tweet);
                context.Entry(tweet).State = EntityState.Modified;
                result = context.SaveChanges() > 0;
                Logger.Log.Debug("user ID:" + tweet.User_Id + " " + userDao.GetByPublisherId(tweet.User_Id).Email + " updated a tweet ID:" + tweet.Id);
            }

            return result;
        }

        public bool Save(int id, string text)
        {
            bool result = false;
            using (context = new TwitterEntities())
            {
                var tweet = GetByPublisherId(id);
                tweet.Body = text;

                context.Entry(tweet).State = EntityState.Modified;
                result = context.SaveChanges() > 0;
                Logger.Log.Debug("user ID:" + tweet.User_Id + " " + userDao.GetByPublisherId(tweet.User_Id).Email + " edited a tweet ID:" + tweet.Id);
            }
            return result;
        }

        public Tweet GetByPublisherId(int id)
        {
            return context.Tweets.FirstOrDefault(x => x.Id == id);
        }
    }
}