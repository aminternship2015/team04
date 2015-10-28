using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using Converter;
using DAL.Entities;
using StaticLogger;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserDao userContext;
        private IFollowService followContext;
        private ITweetService tweetContext;

        public UserService(IUserDao userContext, IFollowService followContext, ITweetService tweetContext)
        {
            this.userContext = userContext;
            this.followContext = followContext;
            this.tweetContext = tweetContext;
        }

        public bool AddNewUser(UserModel user)
        {
            bool result = false;
            try
            {
                result = userContext.Add(UserConverter.ConvertToDB(user));
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
            }
            return result;
        }

        public bool IsEmailUnique(string email)
        {
            bool result = false;
            try
            {
                var allUsers = userContext.GetList();
                result = !allUsers.Any(x => x.Email == email);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
            }
            return result;
        }

        public UserViewModel IsEmailAndPassCorrect(LogInUserViewModel model)
        {
            try
            {
                var curUser = userContext.GetList().FirstOrDefault(x => x.Email == model.Email);
                if (curUser != null)
                {
                    if (curUser.Passwrd == model.Passwrd)
                        return UserConverter.ConvertToViewModel(curUser);
                }
            }
            catch(Exception e)
            {
                Logger.Log.Error(e);
            }
            return null;
        }

        public UserViewModel GetById(int id)
        {
            UserViewModel result = null;
            try 
            {
                User currUser = userContext.GetById(id);
                result = UserConverter.ConvertToViewModel(currUser);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
            }
            return result;
        }

        public List<UserViewModel> GetAll(int currentUserId)
        {
            List<UserViewModel> result = new List<UserViewModel>();
            try
            {
                var allUsers = userContext.GetList();
                foreach (var user in allUsers)
                {
                    UserViewModel convertedUser = UserConverter.ConvertToViewModel(user);
                    convertedUser.IsFollowedByCurrent = (followContext.GetList().Exists(h => h.SubId == currentUserId && h.PubId == user.Id));
                    result.Add(convertedUser);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public void EditUser(UserViewModel user)
        {
            try
            {
                userContext.Update(UserConverter.ConvertViewModelToDB(user));
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var user = userContext.GetById(id);
                userContext.Delete(user);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
        }
    }
}
