using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public  interface IFollowService
    {
        bool Follow(int id, int id2);
        bool UnFollow(int id);
        List<UserViewModel> GetFollows(int id);
        List<FollowViewModel> GetList();
        void DeleteAllFollowWithPublisher(int publisherId);
    }
}
