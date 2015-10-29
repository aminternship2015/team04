using Converter;
using DAL;
using Models;
using StaticLogger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class TweetService : ITweetService
    {
        private ITweetsDao tweetContext;
        private IUserDao userContext;

        public TweetService(ITweetsDao tweetContext, IUserDao userContext)
        {
            this.tweetContext = tweetContext;
            this.userContext = userContext;
        }

        public bool Add(TweetModel tweet)
        {
            bool result = false;
            try
            {
                result = tweetContext.Add(TweetConverter.ConvertToDB(tweet));
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public List<TweetViewModel> GetListById(int userId)
        {
            List<TweetViewModel> result = new List<TweetViewModel>();
            try
            {
                var thisUserTweets = tweetContext.GetList().Where(x => x.User_Id == userId);

                foreach (var tweet in thisUserTweets)
                {
                    var convertedTweet = TweetConverter.ConvertToViewModel(tweet);
                    convertedTweet.Author = userContext.GetByPublisherId(convertedTweet.AuthorId).First_name + " " + userContext.GetByPublisherId(convertedTweet.AuthorId).Last_name;
                    result.Add(convertedTweet);
                }
                result = result.OrderByDescending(x => x.DateAdded).ToList();
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public bool Update(TweetViewModel tweet)
        {
            bool result = false;
            try
            {
                result = tweetContext.Update(TweetConverter.ConvertToDB(tweet));
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public bool Update(int id, string text)
        {
            bool result = false;
            try
            {
                result = tweetContext.Save(id, text);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                result = tweetContext.Delete(id);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public void DeleteAllTweetsByUser(int authorId)
        {
            try
            {
                var tweets = tweetContext.GetList();
                foreach (var item in tweets)
                {
                    if (item.User_Id == authorId) tweetContext.Delete(item.Id);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
        }
    }
}