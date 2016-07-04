using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IAmIt.Service.ProjectService;
using IAmIt.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using MongoDB.Bson;

namespace IAmIt.Controllers
{
    [System.Web.Http.Authorize]
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

        
        [System.Web.Http.HttpPost, System.Web.Http.Route("addProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddProject(AddProjectModel model)
        {
            model.UserId = new ObjectId(User.Identity.GetUserId());
            return Ok(await _service.AddProjectAsync(model));
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getMyProjects"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetMyProjects()
        {
            return Ok(await _service.getMyProjectsAsync(new ObjectId(User.Identity.GetUserId())));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeProject(ChangeProjectModel model)
        {
            await _service.ChangeProjectAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteProject(DeleteProjectModel model)
        {
            await _service.DeleteProjectAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("inviteUserToProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> InviteUserToProject(InviteUserToProjectModel model)
        {
            model.RecipientId = new ObjectId((await UserManager.FindByEmailAsync(model.EmailRecipient)).Id);
            try
            {
                //model.EmailSender = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
                
                await _service.InviteUserToProjectAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("acceptInvitation"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AcceptInvitation(AcceptInvitationModel model)
        {
            try
            {
                model.UserId = new ObjectId(User.Identity.GetUserId());
                await _service.AcceptInvitationAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("rejectInvitation"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> RejectInvitation(RejectInvitationModel model)
        {
            try
            {
                model.UserId = new ObjectId(User.Identity.GetUserId());
                await _service.RejectInvitationAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getAllInvitations"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetAllInvitations()
        {
            return Ok(await _service.GetAllInvitationsAsync(new ObjectId(User.Identity.GetUserId())));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteUserFromProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteUserFromProject(DeleteUserFromProjectModel model)
        {
            model.UserId = new ObjectId((await UserManager.FindByEmailAsync(model.UserEmail)).Id);
            await _service.DeleteUserFromProjectAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetProject(GetProjectModel model)
        {
            return Ok(await _service.GetProjectAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteYourselfFromProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteYourself(DeleteYourselfFromProjectModel model)
        {
            try
            {
                model.UserId = new ObjectId(User.Identity.GetUserId());
                await _service.DeleteYourselfAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getAllUsersInProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetAllUsersInProject(GetProjectModel model)
        {
            return Ok((await _service.GetAllUsersInProjectAsync(model)).
                Select(u => new UserToSendModel
                {
                    Email = (UserManager.FindById(u.ToString())).Email,
                    Name = (UserManager.FindById(u.ToString())).FullName
                }).ToList());
        }
    }
}
