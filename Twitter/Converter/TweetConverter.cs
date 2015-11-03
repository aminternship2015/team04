using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class TweetConverter
    {
        public static Tweet ConvertToDB(TweetModel tweet)
        {
            var newTweet = new Tweet
            {
                User = new User(),
                User_Id = tweet.User_Id,
                Body = tweet.Body,
                Date_time = DateTime.Now
            };

            return newTweet;
        }

        public static Tweet ConvertToDB(TweetViewModel tweet)
        {
            var newTweet = new Tweet
            {
                Id = tweet.Id,
                User = new User(),
                User_Id = tweet.User_Id,
                Body = tweet.Body,
                Date_time = DateTime.Now
            };

            return newTweet;
        }

        public static TweetViewModel ConvertToViewModel(Tweet tweet)
        {
            var newTweet = new TweetViewModel
            {
                Id = tweet.Id, 
                User_Id = tweet.User_Id,
                Body = tweet.Body,
                DateAdded = tweet.Date_time,
                AuthorId = tweet.User_Id
            };

            return newTweet;
        }
    }
}
