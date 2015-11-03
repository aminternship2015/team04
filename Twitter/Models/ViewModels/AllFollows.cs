using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class AllFollows
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<UserViewModel> CurrentUserFollows { get; set; }
    }
}
