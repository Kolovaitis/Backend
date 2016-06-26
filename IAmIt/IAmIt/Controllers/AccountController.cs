using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using IAmIt.Models;
using IAmIt.Service.ProjectService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace IAmIt.Controllers
{
    [System.Web.Http.Authorize]
    public class AccountController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IProjectService _service;

        public AccountController(IProjectService service)
        {
            _service = service;
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("login"), ValidateAntiForgeryToken, System.Web.Http.AllowAnonymous]
        public async Task<IHttpActionResult> Login(UserLoginModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var userIdentity = await user.GenerateUserIdentityAsync(UserManager);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, userIdentity);
                return Ok("Ok");
            }
            return BadRequest("Invalid email or password");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("registration"), System.Web.Http.AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Registration(UserRegistrationModel model)
        {
            if (await UserManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("User with this email already exist");
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.Name };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errorsEnumerator = result.Errors.GetEnumerator();
                errorsEnumerator.MoveNext();
                var errorString = errorsEnumerator.Current;
                while (errorsEnumerator.MoveNext())
                {
                    errorString += (" " + errorsEnumerator.Current);
                }
                return BadRequest(errorString);
            }
            return Ok("Ok");
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("logoff"), ValidateAntiForgeryToken]
        public IHttpActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeInfo"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeInfo(UserChangeInfoModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (model.Name != null)
                user.FullName = model.Name;
            await UserManager.UpdateAsync(user);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeCredentials"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeCredentials(UserChangeCredentialsModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null && await UserManager.CheckPasswordAsync(user, model.OldPassword))
            {
                if (model.NewEmail != null)
                {
                    if (await UserManager.FindByEmailAsync(model.NewEmail) != null)
                        return BadRequest("User with this email already exist");
                    user.Email = model.NewEmail;
                    user.UserName = model.NewEmail;
                    await UserManager.UpdateAsync(user);
                }
                if (model.NewPassword != null)
                {
                    var result = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        var errorsEnumerator = result.Errors.GetEnumerator();
                        errorsEnumerator.MoveNext();
                        var errorString = errorsEnumerator.Current;
                        while (errorsEnumerator.MoveNext())
                        {
                            errorString += (" " + errorsEnumerator.Current);
                        }
                        return BadRequest(errorString);
                    }
                }
                return Ok("Ok");
            }
            return BadRequest("Invalid password");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getUser"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetUser(UserOnlyEmailModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid email");
            return Ok(new UserToSendModel { Email = user.Email, Name = user.FullName });
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("myProfile"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> MyProfile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return Ok(new UserToSendModel { Email = user.Email, Name = user.FullName });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }
    }
}
