using System;
using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter a Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter a Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
