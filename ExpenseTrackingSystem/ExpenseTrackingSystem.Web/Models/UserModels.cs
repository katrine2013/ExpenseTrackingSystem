using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackingSystem.Web.Models
{
    public class UserModels
    {
        public class LoginUserModel
        {
            [Required(ErrorMessage = "Please, write your login!")]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Please, write your password. Check language of the typing and pressing key 'CAPSLOCK'")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class RegisterUserModel
        {
            [Required(ErrorMessage = "Please, write your future login.")]
            [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
            public string Login { get; set; }

            [Required(ErrorMessage = "Please, write your password!")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Please, confirm your password")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}