using Models;
using Models.ViewModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StaticLogger;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private ITweetService tweetService;
        private IUserService userService;
        private IFollowService followService;

        public UserController(ITweetService _tweetService, IUserService _userService, IFollowService _followService)
        {
            this.tweetService = _tweetService;
            this.userService = _userService;
            this.followService = _followService;
        }

        public ActionResult All(int? page = 1)
        {
            AllViewModel allModel = new AllViewModel();
            
            var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            var allUsers = userService.GetAll(currentUser.Id);
            allUsers.Remove(allUsers.Find(x => x.Id == currentUser.Id));

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            allModel.Users = allUsers.ToPagedList(pageNumber, pageSize);
            allModel.TotalUsers = allUsers.Count;

            Logger.Log.Debug("all users are displayed for ID:" + currentUser.Id + " " + currentUser.Email);
            return View(allModel);
        }

        public ActionResult Info(int id, int? page = 1)
        {
            var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            var curUser = userService.GetById(id);

            if (id == currentUser.Id)
                return RedirectToAction("UserPage");

            var infoModel = new InfoViewModel();

            infoModel.CurrentUserFollows = followService.GetList();
            infoModel.Users = userService.GetAll(currentUser.Id);
                
            UserViewModel thisUser = userService.GetById(id);
            var tweets = tweetService.GetListById(thisUser.Id);
            infoModel.User = curUser;

            infoModel.TweetsCount = tweets.Count;

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            int totalItems = tweets.Count;
            infoModel.Tweets = tweets.ToPagedList(pageNumber, pageSize);

            Logger.Log.Debug("information about user ID:" + curUser.Id + " " + curUser.Email + " are displayed");
            return View(infoModel);
        }

        public ActionResult UserPage(int? page = 1)
        {
            var infoModel = new InfoViewModel();
            var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            var curUser = userService.GetById(currentUser.Id);

            infoModel.CurrentUserFollows = followService.GetList();

            var tweets = tweetService.GetListById(currentUser.Id);
            infoModel.TweetsCount = tweets.Count;
            infoModel.User = curUser;

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            int totalItems = tweets.Count;
            infoModel.Tweets = tweets.ToPagedList(pageNumber, pageSize);

            Logger.Log.Debug("user " + currentUser.Id + " " + currentUser.Email + " entered on his userpage");
            return View(infoModel);
        }

        public bool Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (userService.IsEmailUnique(user.Email))
                {
                    userService.EditUser(user);
                    var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
                    Logger.Log.Debug("user  ID:" + currentUser.Id + " " + currentUser.Email + " updated own information");
                }
                else { return false; }
                return true;
            }
            return false;
        }

        public ActionResult UsersProfile()
        {
            var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            var user = userService.GetById(currentUser.Id);

            HttpContext.Session["CurrentUser"]  = user;
            Logger.Log.Debug("user  ID:" + currentUser.Id + " " + currentUser.Email + " has entered on his own profile");

            return View(user);
        }

        public ActionResult Follow(int publisherId, int subsriberId)
        {
            var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            subsriberId = currentUser.Id;
            followService.Follow(publisherId, subsriberId);
            Logger.Log.Debug("user ID:" + subsriberId + " now follows user ID:" + publisherId);

            return View();
        }

        public ActionResult Unfollow(int id)
        {
            followService.UnFollow(id);
            return View();
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteCurrentUser()
        {
            var currentUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            followService.DeleteAllFollowWithPublisher(currentUser.Id);
            tweetService.DeleteAllTweetsByUser(currentUser.Id);
            userService.Delete(currentUser.Id);
            HttpContext.Session["CurrentUser"] = null;          
            return RedirectToAction("Index", "Home");
        }
    }
}
