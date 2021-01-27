using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestASP.Models.HelpModel;

namespace TestASP.Settings.ValidationSettings
{
    public class CustomPasswordValidation : IPasswordValidator<User>
    {
        public int RequiredLength { get; set; } //min Lenght

        public CustomPasswordValidation(int length)
        {
            RequiredLength = length;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            var errors = new List<IdentityError>();

            if (String.IsNullOrEmpty(password) || password.Length < RequiredLength)
            {
                errors.Add(new IdentityError
                {
                    Description = $"Minimum password length is  {RequiredLength}"
                });
            }
            var passwordTemplate = "^[a-zA-Z0-9]+$";
            
            if (!Regex.IsMatch(password, passwordTemplate))
            {
                errors.Add(new IdentityError
                {
                    Description = "Password must be contained numbers and/or letters"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
