using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace Models.ViewModels
{
    public class AllViewModel
    {
        public IPagedList<UserViewModel> Users { get; set; }
        public int TotalUsers {get; set;}
    }
}
