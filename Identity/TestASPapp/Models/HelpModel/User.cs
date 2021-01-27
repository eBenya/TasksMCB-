using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestASP.Models.HelpModel
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }

    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Year { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Display(Name = "Old password")]
        public string OldPassword { get; set; }
    }
}
