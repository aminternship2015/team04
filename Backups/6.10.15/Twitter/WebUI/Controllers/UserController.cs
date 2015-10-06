﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService _userService)
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
                if (userService.IsUsernameUnique(newUser.Username))
                {
                    if (userService.IsEmailUnique(newUser.Email))
                    {
                        if (userService.AddNewUser(newUser))
                        {
                            return RedirectToAction("LogIn", "User");
                        }
                    }
                    else
                    {
                        ViewBag.errorMessage = "Email is already in use!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.errorMessage = "Username is already in use!";
                    return View();
                }
            }
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
                if (userService.IsUsernamePassCorrect(currentUser))
                {
                    HttpContext.Session["CurrentUser"] = currentUser;
                    return RedirectToAction("Newsfeed", "Tweet");
                }
                else
                {
                    ViewBag.errorMessage = "Username or Password is incorrect!";
                    return View();
                }
            }
            return View();
        }
    }
}