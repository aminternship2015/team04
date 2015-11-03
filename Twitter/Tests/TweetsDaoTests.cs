using DAL;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TweetsDaoTests
    {
        TweetsDao tweetDao;
        UserDao userDao;
        User testUser;

        [TestInitialize]
        public void Initialize()
        {
            userDao = new UserDao();
            tweetDao = new TweetsDao(userDao);
            testUser = new User()
            {
                First_name = "Vlad Guleaev",
                Email = "test@test.com",
                Passwrd = "123",
                Last_name = "testUser"
            };
        }

        [TestMethod]
        public void Add_NewTweet_True()
        {        
            //arrange
            userDao.Add(testUser);
            Tweet newTweet = new Tweet()
            {
                Body = "testTweet",
                Date_time = DateTime.Now,
                User_Id = testUser.Id,
                User = testUser              
            };

            //act   
            var Actual = tweetDao.Add(newTweet);

            //delete
            tweetDao.Delete(newTweet);
            userDao.Delete(testUser);

            //asert
            Assert.IsTrue(Actual);
        }

        [TestMethod]
        public void Delete_Tweet_True()
        {
            //arrange
            userDao.Add(testUser);
            
            Tweet newTweet = new Tweet()
            {
                Body = "testTweet",
                Date_time = DateTime.Now,
                User_Id = testUser.Id,
                User = testUser
            };

            tweetDao.Add(newTweet);

            //act
            var Actual = tweetDao.Delete(newTweet);

            //delete
            userDao.Delete(testUser);

            //asert
            Assert.IsTrue(Actual);
        }

        [TestMethod]
        public void Update_Tweet_True()
        {
            //arrange
            userDao.Add(testUser);

            Tweet newTweet = new Tweet()
            {
                Body = "testTweet",
                Date_time = DateTime.Now,
                User_Id = testUser.Id,
                User = testUser,

            };

            tweetDao.Add(newTweet);

            //act
            newTweet.Body = "changedTweet";
            var Actual = tweetDao.Update(newTweet);

            //delete
            tweetDao.Delete(newTweet);
            userDao.Delete(testUser);

            //asert
            Assert.IsTrue(Actual);
        }
    }
}
