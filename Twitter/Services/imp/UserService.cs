using Converter;
using DAL;
using DAL.Entities;
using Models;
using System.Linq;

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
            var allUsers = userContext.GetList();

            return !allUsers.Any(x => x.Username == username);
        }

        public bool IsEmailUnique(string email)
        {
            var allUsers = userContext.GetList();

            return !allUsers.Any(x => x.Email == email);
        }

        public User IsUsernamePassCorrect(LogInUserModel model)
        {
            var curUser = userContext.GetByUsername(model.Username);

            curUser.Tweets = curUser.Tweets;

            if (curUser != null)
            {
                return curUser;
            }

            return null;
        }
    }
}
