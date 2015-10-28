using DAL.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        bool AddNewUser(UserModel user);
        bool IsEmailUnique(string email);
        UserViewModel IsEmailAndPassCorrect(LogInUserViewModel currentUser);
        UserViewModel GetById(int id);
        List<UserViewModel> GetAll(int currentUserId);
        void EditUser(UserViewModel user);
        void Delete(int id);
    }
}
