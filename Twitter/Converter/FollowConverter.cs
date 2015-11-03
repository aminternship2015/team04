using DAL.Entities;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class FollowConverter
    {
        public static FollowViewModel ConvertToFollow(Follow fol)
        {
            var followViewModel = new FollowViewModel
            {
                PubId = fol.Publisher_Id,
                SubId = fol.Subscriber_Id
            };

            return followViewModel;
        }   
    }
}
