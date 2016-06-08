using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectPool.Models
{
    public class LoginViewModel
    {
        public int userId { get; set; }

        public string returnUrl { get; set; }
        public bool rememberMe { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "*")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        public string password { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        public string confirmPassword { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        public string newPassword { get; set; }

        public string GUID { get; set; }
    }
}