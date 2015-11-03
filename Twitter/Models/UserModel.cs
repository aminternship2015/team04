using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography; 

namespace Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter your First Name")]
        [MaxLength(60, ErrorMessage = "First name should contain maximum of 60 characters")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        [MaxLength(60, ErrorMessage = "Last Name should contain maximum of 60 characters")]
        public string Last_name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        [MaxLength(100, ErrorMessage = "Email should contain maximum of 100 characters")]
        public string Email { get; set; }
       
        //[StringLength(60,MinimumLength= 8, ErrorMessage = "Minimum 8 characters")]
        //[MinLength(3, ErrorMessage = "Password must be at least 6 characters")]
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(60, MinimumLength = 5,ErrorMessage="5 chars minimum length!")]
        public string Passwrd 
        {
            get { return password; }
            set { password = new PasswordEncryption().GetHashString(value); }
        }
        private string password;
    }

}
