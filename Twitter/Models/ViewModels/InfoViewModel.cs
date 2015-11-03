using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.ViewModels
{
    public class InfoViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<FollowViewModel> CurrentUserFollows { get; set; }
        public IPagedList<TweetViewModel> Tweets { get; set; }
        public UserViewModel User { get; set; }
        public int TweetsCount { get; set; }
    }
}
