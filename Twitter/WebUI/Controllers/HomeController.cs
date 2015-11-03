using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System;
using Services;
namespace WebUI.Controllers
{   
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ITweetService tweetS;

        public HomeController(ITweetService _tweetS)
        {
            this.tweetS = _tweetS;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session["CurrentUser"] != null)
            {
                return RedirectToAction("Newsfeed", "Tweet");
            }
            else
                return View();
        }
    }
}