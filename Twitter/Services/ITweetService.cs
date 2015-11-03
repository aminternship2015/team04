using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITweetService
    {
        bool Add(TweetModel tweet);
        bool Update(TweetViewModel tweet);
        bool Update(int id, string text);
        bool Delete(int id);
        List<TweetViewModel> GetListById(int userId);
        void DeleteAllTweetsByUser(int authorId);
    }
}
