using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    [TestClass]
    public class TweetControllerTests
    {
        FirefoxDriver firefox;

        [TestMethod]
        public void Add_Tweet_FindNewTweetInNewsfeed()
        {
            firefox = new FirefoxDriver();
            firefox.Navigate().GoToUrl("http://localhost:57336/");
            firefox.FindElement(By.Id("register")).Click();
            //firefox.FindElement(By.Id("login")).Click();

            //register page
            firefox.FindElement(By.Id("inputFirstName")).SendKeys("testFirstName");
            firefox.FindElement(By.Id("inputLastName")).SendKeys("testLastName");
            firefox.FindElement(By.Id("inputEmail")).SendKeys("test2@mail.com");
            firefox.FindElement(By.Id("inputPassword")).SendKeys("123");
            firefox.FindElement(By.Id("submit")).Click();
            firefox.FindElement(By.Id("login")).Click();

            //log in page
            firefox.FindElement(By.Id("inputEmail")).SendKeys("test2@mail.com");
            firefox.FindElement(By.Id("inputPassword")).SendKeys("123");
            firefox.FindElement(By.Id("login")).Click();

            firefox.FindElement(By.Name("Body")).Click();
            firefox.FindElement(By.Name("Body")).SendKeys("test Tweet");

            firefox.FindElement(By.Id("tweetbutton")).Click();
            firefox.FindElement(By.Id("home")).Click();
            firefox.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); 

            IWebElement firstTweet = firefox.FindElement(By.Id("tweet 1"));
            string tweetMessage = firstTweet.Text;

            //delete test user
            firefox.Navigate().GoToUrl("http://localhost:57336/User/UsersProfile");
            firefox.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            firefox.FindElement(By.Id("deleteProfile")).Click();

            Assert.IsTrue(tweetMessage == "test Tweet");
        }

        [TestCleanup]
        public void TearDown()
        {
            firefox.Quit();
        }
    }
}
