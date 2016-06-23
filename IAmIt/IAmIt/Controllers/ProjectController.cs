using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IAmIt.Service.ProjectService;
using IAmIt.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace IAmIt.Controllers
{
    [Authorize]
    public class ProjectController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        
        [System.Web.Http.HttpPost, System.Web.Http.Route("addProject")]
        public async Task<IHttpActionResult> AddProject(AddProjectModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            return Ok(await _service.AddProjectAsync(model));
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getMyProjects")]
        public async Task<IHttpActionResult> GetMyProjects()
        {
            return Ok(await _service.getMyProjectsAsync((await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeProject")]
        public async Task<IHttpActionResult> ChangeProject(ChangeProjectModel model)
        {
            await _service.ChangeProjectAsync(model);
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteProject")]
        public async Task<IHttpActionResult> DeleteProject(DeleteProjectModel model)
        {
            await _service.DeleteProjectAsync(model);
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("inviteUserToProject")]
        public async Task<IHttpActionResult> InviteUserToProject(InviteUserToProjectModel model)
        {
            //model.EmailSender = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            await _service.InviteUserToProjectAsync(model);
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("acceptInvitation")]
        public async Task<IHttpActionResult> AcceptInvitation(AcceptInvitationModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            await _service.AcceptInvitationAsync(model);
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("rejectInvitation")]
        public async Task<IHttpActionResult> RejectInvitation(RejectInvitationModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            await _service.RejectInvitationAsync(model);
            return Ok();
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getAllInvitations")]
        public async Task<IHttpActionResult> GetAllInvitations()
        {
            return Ok(_service.GetAllInvitationsAsync((await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email).Result);
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteUserFromProject")]
        public async Task<IHttpActionResult> DeleteUserFromProject(DeleteUserFromProjectModel model)
        {
            await _service.DeleteUserFromProjectAsync(model);
            return Ok();
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getProject")]
        public async Task<IHttpActionResult> GetProject(GetProjectModel model)
        {
            return Ok(await _service.GetProjectAsync(model));
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
