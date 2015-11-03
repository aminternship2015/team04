using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TweetModel
    {
        public int Id { get; set; }
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please enter tweet body!")]
        [StringLength(140, ErrorMessage = "Tweet should contain maximum of 140 characters!")]
        public string Body { get; set; }

        public System.DateTime Date_time { get; set; }

        public virtual UserModel User { get; set; }

    }
}
