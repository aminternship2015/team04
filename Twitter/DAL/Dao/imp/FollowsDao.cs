using DAL.Entities;
using StaticLogger;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class FollowsDao : IFollowsDao
    {
        private TwitterEntities context;
        private IUserDao user;

        public FollowsDao(IUserDao _user)
        {
            this.user = _user;
        }

        public bool Add(int publisherId, int subscriberId)
        {
            bool result = false;

            var follow = new Follow
            {
                Publisher_Id = publisherId,
                Subscriber_Id = subscriberId,
                User = user.GetByPublisherId(publisherId),
                User1 = user.GetByPublisherId(subscriberId)
            };

            using (var context = new TwitterEntities())
            {
                context.Follows.Attach(follow);
                context.Follows.Add(follow);

                result = context.SaveChanges() > 0;
                Logger.Log.Debug("A new follow pair created with publisherId " + follow.Publisher_Id + " and subscriberId " + follow.Subscriber_Id);
            }
            return result;
        }

        public ICollection<User> GetUserFollows(int currentUserId)
        {
            List<User> result = new List<User>();

            using (var context = new TwitterEntities())
            {
                var xd = context.Follows.Where(x => x.Subscriber_Id == currentUserId && x.Publisher_Id != null);

                foreach (var item in xd)
                {
                    result.Add(user.GetByPublisherId(item.Publisher_Id));
                }
            }
            return result.ToList();
        }

        public ICollection<Follow> GetList()
        {
            ICollection<Follow> result;
            using (var context = new TwitterEntities())
            {
                result = context.Follows.ToList();
            }
            return result;
        }

        public Follow GetByPublisherId(int id)
        {
            var context = new TwitterEntities();
            return context.Follows.FirstOrDefault(x => x.Publisher_Id == id);
        }

        public Follow GetById(int id)
        {
            var context = new TwitterEntities();
            return context.Follows.FirstOrDefault(x => x.Id == id);
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var context = new TwitterEntities())
            {
                var follow = GetByPublisherId(id);

                context.Follows.Attach(follow);
                context.Follows.Remove(follow);
                result = context.SaveChanges() > 0;
                Logger.Log.Debug("A new follow pair deleted with publisherId " + follow.Publisher_Id + " and subscriberId " + follow.Subscriber_Id);
            }
            return result;
        }

        public bool DeleteById(int id)
        {
            bool result = false;
            using (var context = new TwitterEntities())
            {
                var follow = GetById(id);

                context.Follows.Attach(follow); 
                context.Follows.Remove(follow);
                result = context.SaveChanges() > 0;
                Logger.Log.Debug("A new follow pair deleted with publisherId " + follow.Publisher_Id + " and subscriberId " + follow.Subscriber_Id);
            }
            return result;
        }
    }
}