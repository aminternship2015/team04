﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using Converter;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserDao userContext;

        public UserService(IUserDao userContext)
        {
            this.userContext = userContext;
        }

        public bool AddNewUser(UserModel user)
        {
            return userContext.Add(UserConverter.ConvertToDB(user));
        }

        public bool IsUsernameUnique(string username)
        {
            return !userContext.IsUsernameExists(username);
        }

        public bool IsEmailUnique(string email)
        {
            return !userContext.IsEmailExists(email);
        }

        public bool IsUsernamePassCorrect(LogInUserModel model)
        {
            var curUser = userContext.GetByUsername(model.Username);
            if (curUser != null)
            {
                return curUser.Passwrd == model.Passwrd;
            }
            return false;
        }
    }
}