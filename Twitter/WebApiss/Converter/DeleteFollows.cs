using DAL.Entities;
using StaticLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Converter
{
    public class DeleteFollows
    {
        private TwitterEntities db = new TwitterEntities();

        public void DeleteAllFollowWithPublisher(int publisherId)
        {
            Follow follow;
            try
            {
                var followlist = db.Follows.ToList() ;
                foreach (var item in followlist)
                {
                    if (item.Publisher_Id == publisherId || item.Subscriber_Id == publisherId)
                    {
                        follow = db.Follows.Find(item.Id);
                        db.Follows.Attach(follow);
                        db.Follows.Remove(follow);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                //Logger.Log.Error(e.Message);
            }   
        }


        public void DeleteAllTweetsByUser(int authorId)
        {
            Tweet tweet;

            try
            {
                var tweets = db.Tweets.ToList();
                foreach (var item in tweets)
                {
                    if (item.User_Id == authorId)
                    {
                        tweet = db.Tweets.Find(item.Id);
                        db.Tweets.Remove(tweet);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                //Logger.Log.Error(e.Message);
            }
        }
    }
}