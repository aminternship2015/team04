using DAL.Entities;
using Models;

namespace Services
{
    public interface IUserService
    {
        bool AddNewUser(UserModel user);
        bool IsUsernameUnique(string username);
        bool IsEmailUnique(string email);
        User IsUsernamePassCorrect(LogInUserModel currentUser);
    }
}
