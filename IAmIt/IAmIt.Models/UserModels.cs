using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;

namespace IAmIt.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserChangeCredentialsModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewEmail { get; set; }
    }

    public class UserChangeInfoModel
    {
        public string Name { get; set; }
    }

    public class UserOnlyEmailModel
    {
        public string Email { get; set; }
    }

    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UserToSendModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
