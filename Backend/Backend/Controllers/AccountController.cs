
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Security;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Backend.Models;
using Backend.Models.UserModels;

namespace Backend.Controllers
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



        [System.Web.Http.HttpPost, System.Web.Http.Route("login"), ValidateAntiForgeryToken, System.Web.Http.AllowAnonymous]
        public async Task<IHttpActionResult> Login(UserLoginModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var userIdentity = await user.GenerateUserIdentityAsync(UserManager);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, userIdentity);
                return Ok();
            }
            return BadRequest("invalid email or password");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("registration"), System.Web.Http.AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Registration(UserRegistrationModel model)
        {
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
            return Ok();
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("logoff"), ValidateAntiForgeryToken]
        public IHttpActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeInfo"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeInfo(UserChangeInfoModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (model.Name != null)
                user.FullName = model.Name;
            await UserManager.UpdateAsync(user);
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeCredentials"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeCredentials(UserChangeCredentialsModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                if (await UserManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    if (model.NewEmail != null)
                    {
                        if (await UserManager.FindByEmailAsync(model.NewEmail) != null)
                            return BadRequest("user with this email already exist");
                        user.Email = model.NewEmail;
                        user.UserName = model.NewEmail;
                        await UserManager.UpdateAsync(user);
                    }
                    if (model.NewPassword != null)
                        await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);
                    return Ok();
                }
            }
            return BadRequest("invalid password");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getUser"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetUser(UserOnlyEmailModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("invalid email");
            return Ok(new UserToSendModel { Email = user.Email, FullName = user.FullName });
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("myProfile"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> MyProfile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return Ok(new UserToSendModel { Email = user.Email, FullName = user.FullName });
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

