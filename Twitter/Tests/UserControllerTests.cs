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
    public class UserController
    {
        FirefoxDriver firefox;

        [TestMethod]
        public void SingUp_TestUser_FindNameInHelloString()
        {
            firefox = new FirefoxDriver();
            firefox.Navigate().GoToUrl("http://localhost:57336/");
            firefox.FindElement(By.Id("register")).Click();

            //register page
            firefox.FindElement(By.Id("inputFirstName")).SendKeys("testFirstName");
            firefox.FindElement(By.Id("inputLastName")).SendKeys("testLastName");
            firefox.FindElement(By.Id("inputEmail")).SendKeys("test@mail.com");
            firefox.FindElement(By.Id("inputPassword")).SendKeys("123");
            firefox.FindElement(By.Id("submit")).Click();
            firefox.FindElement(By.Id("login")).Click();

            //log in page
            firefox.FindElement(By.Id("inputEmail")).SendKeys("test@mail.com");
            firefox.FindElement(By.Id("inputPassword")).SendKeys("123");
            firefox.FindElement(By.Id("login")).Click();

            firefox.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            string helloString = firefox.FindElement(By.Id("helloString")).Text;
            string result = "Hello, testFirstName testLastName";

            //delete test user
            firefox.Navigate().GoToUrl("http://localhost:57336/User/UsersProfile");
            firefox.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            firefox.FindElement(By.Id("deleteProfile")).Click();

            Assert.IsTrue(result == helloString);
        }

        [TestCleanup]
        public void TearDown()
        {
            firefox.Quit();
        }
    }
}
