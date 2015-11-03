using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;
using WebUI.App_Start;
using StaticLogger;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController(IUserService _userService)
        {
            this.userService = _userService;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel newUser)
        {
            if (ModelState.IsValid)
            {
                if (userService.IsEmailUnique(newUser.Email))
                {
                    userService.AddNewUser(newUser);
                    Logger.Log.Debug("new user " + newUser.Email + " has registered");
                    return RedirectToAction("RegisterSuccess", "Account");
                }
                ViewBag.errorMessage = "Email is already in use!";
            }
            return View();
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult DeletedSuccess()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInUserViewModel currentUser)
        {
            if (ModelState.IsValid)
            {
                var enteredUser = userService.IsEmailAndPassCorrect(currentUser);
                if (enteredUser != null)
                {
                    HttpContext.Session["CurrentUser"] = enteredUser;
                    Logger.Log.Debug("user ID:" + enteredUser.Id + " " + enteredUser.Email + " logged in");
                    return RedirectToAction("Newsfeed", "Tweet");
                }
                else
                {
                    ViewBag.errorMessage = "Email or Password is incorrect!";
                    return View();
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            var leftUser = (UserViewModel)HttpContext.Session["CurrentUser"];
            Logger.Log.Debug("user ID:" + leftUser.Id + " " + leftUser.Email + " logged out");
            HttpContext.Session["CurrentUser"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}