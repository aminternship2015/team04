using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.Entities;
using Models.ViewModels;
using Converter;

namespace Converter
{
    public static class UserConverter
    {
        public static User ConvertToDB(UserModel user)
        {
            var newUser = new User { 
                First_name = user.First_name, 
                Last_name = user.Last_name, 
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
                 First_name = user.First_name,
                 Last_name = user.Last_name,
                 Email = user.Email
            };

            return userViewModel;
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
                result.Add(FollowConverter.ConvertToFollow(item));
            }
            return result;
        }

        public static User ConvertViewModelToDB(UserViewModel user)
        {
            var newUser = new User
            {
                First_name = user.First_name,
                Last_name = user.Last_name,
                Email = user.Email,
                Id = user.Id,
            };

            return newUser;
        }
    }
}
