using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Converter
{
    public class UserConverter
    {
        public List<UserApiModel> ApiUsers(List<User> users)
        {
            var _users = new List<UserApiModel>();

            foreach (var item in users)
            {
                _users.Add(ApiUser(item));
            }

            return _users;
        }

        public UserApiModel ApiUser(User user)
        {
            var apiuser = new UserApiModel
            {
                Id = user.Id,
                Email = user.Email,
                First_name = user.First_name,
                Last_name = user.Last_name
            };

            return apiuser;
        }

        public User ApiModelToDBModel(UserApiModel user)
        {
            var dbUser = new User
            {
                Id=user.Id,
                Email = user.Email,
                First_name = user.First_name,
                Last_name = user.Last_name,
                Passwrd = "123"
            };

            return dbUser;
        }

    }
}