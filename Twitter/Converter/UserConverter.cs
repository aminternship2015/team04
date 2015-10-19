﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.Entities;
using Models.ViewModels;

namespace Converter
{
    public static class UserConverter
    {
        public static User ConvertToDB(UserModel user)
        {
            var newUser = new User { 
                FullName = user.FullName, 
                Username = user.Username, 
                Email = user.Email, 
                Passwrd = user.Passwrd 
            };

            return newUser;
        }

        public static User ConvertToDB(LogInUserViewModel user)
        {
            var currentUser = new User
            {
                Id = user.Id,
                Email = user.Email,
                Passwrd = user.Passwrd
            };

            return currentUser;
        }

        public static UserViewModel ConvertToViewModel(User user)
        {
            var userViewModel = new UserViewModel
            {
                 Id = user.Id,
                 FullName = user.FullName,
                 Username = user.Username,
                 Email = user.Email
            };

            return userViewModel;
        }

        public static FollowViewModel ConvertToFollow(Follow fol)
        {
            var followViewModel = new FollowViewModel
            {
                PubId = fol.Publisher_Id,
                SubId = fol.Subscriber_Id
            };

            return followViewModel;
        }   

        public static List<UserViewModel> ConvertViewModelList(IEnumerable<User> users)
        {
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var item in users)
            {
                result.Add(ConvertToViewModel(item));
            }
            return result;
        }

        public static List<FollowViewModel> ConvertFollowModel(ICollection<Follow> follows)
        {
            List<FollowViewModel> result = new List<FollowViewModel>();
            foreach (var item in follows)
            {
                result.Add(ConvertToFollow(item));
            }
            return result;
        }
    }
}
