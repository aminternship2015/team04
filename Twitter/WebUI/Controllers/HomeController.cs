using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System;
using Services;
using DAL.Entities;
namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ITweetService tweetS;

        public HomeController(ITweetService _tweetS)
        {
            this.tweetS = _tweetS;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Newsfeed()
        {
            var CurrentUser = HttpContext.Session["CurrentUser"] as User;
            return View(CurrentUser.Tweets);
        }

        [HttpPost]
        public ActionResult Newsfeed(TweetModel tweet)
        {
            var CurrentUser = HttpContext.Session["CurrentUser"] as User;
            if (ModelState.IsValid)
            {
                TweetModel test = new TweetModel() { Body = tweet.Body, Date_time = DateTime.Now, User_Id = CurrentUser.Id};
                tweetS.AddNewTweet(test);
                return RedirectToAction("Newsfeed", "Home");

            }
            return View();
        }
    }
}