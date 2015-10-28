using Converter;
using DAL;
using Models;
using Models.ViewModels;
using StaticLogger;
using System;
using System.Collections.Generic;

namespace Services
{
    public class FollowService : IFollowService
    {
        private IFollowsDao context;

        public FollowService(IFollowsDao _context)
        {
            this.context = _context;
        }

        public bool Follow(int publisherId, int subscriberId)
        {
            bool result = false;
            try
            {
                result = context.Add(publisherId, subscriberId);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public bool UnFollow(int id)
        {
            bool result = false;
            try
            {
                result = context.Delete(id);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
            return result;
        }

        public List<UserViewModel> GetFollows(int id)
        {
            List<UserViewModel> result = new List<UserViewModel>();
            result = UserConverter.ConvertViewModelList(context.GetUserFollows(id));

            return result;
        }

        public List<FollowViewModel> GetList()
        {
            List<FollowViewModel> followlist = new List<FollowViewModel>();
            try
            {
                followlist = UserConverter.ConvertFollowModel(context.GetList());
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }       
            return followlist;
        }

        public void DeleteAllFollowWithPublisher(int publisherId)
        {
            try
            {
                var followlist = context.GetList();
                foreach (var item in followlist)
                {
                    if (item.Publisher_Id == publisherId || item.Subscriber_Id == publisherId) 
                        context.Delete(item.Id);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }   
        }
    }
}