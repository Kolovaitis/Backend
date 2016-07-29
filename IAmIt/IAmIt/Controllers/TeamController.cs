using IAmIt.Hubs;
using IAmIt.Models;
using IAmIt.Service.TeamService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace IAmIt.Controllers
{
    [System.Web.Http.Authorize]
    public class TeamController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        public IHubContext HubContext
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            }
        }
        private readonly  ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }
        [System.Web.Http.HttpPost, System.Web.Http.Route("addTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddTeam(AddTeamModel model)
        {
            model.UserId = new ObjectId(User.Identity.GetUserId());
            return Ok(await _service.AddTeamAsync(model));
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getMyTeams"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetMyTeams()
        {
            return Ok(await _service.getMyTeamsAsync(new ObjectId(User.Identity.GetUserId())));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeTeam(ChangeTeamModel model)
        {
            await _service.ChangeTeamAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteTeam(DeleteTeamModel model)
        {
            await _service.DeleteTeamAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("inviteUserToTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> InviteUserToTeam(InviteUserToTeamModel model)
        {
            model.RecipientId = new ObjectId((await UserManager.FindByEmailAsync(model.EmailRecipient)).Id);
            try
            {
                //model.EmailSender = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;

                await _service.InviteUserToTeamAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("acceptTeamInvitation"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AcceptTeamInvitation(AcceptTeamInvitationModel model)
        {
            try
            {
                model.UserId = new ObjectId(User.Identity.GetUserId());
                await _service.AcceptTeamInvitationAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("rejectTeamInvitation"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> RejectTeamInvitation(RejectTeamInvitationModel model)
        {
            try
            {
                model.UserId = new ObjectId(User.Identity.GetUserId());
                await _service.RejectTeamInvitationAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getAllTeamInvitations"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetAllTeamInvitations()
        {
            return Ok(await _service.GetAllTeamInvitationsAsync(new ObjectId(User.Identity.GetUserId())));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteUserFromTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteUserFromTeam(DeleteUserFromTeamModel model)
        {
            model.UserId = new ObjectId((await UserManager.FindByEmailAsync(model.UserEmail)).Id);
            await _service.DeleteUserFromTeamAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetTeam(GetTeamModel model)
        {
            return Ok(await _service.GetTeamAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteYourselfFromTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteYourself(DeleteYourselfFromTeamModel model)
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

        [System.Web.Http.HttpPost, System.Web.Http.Route("getAllUsersInTeam"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetAllUsersInTeam(GetTeamModel model)
        {
            return Ok((await _service.GetAllUsersInTeamAsync(model)).
                Select(u => new UserToSendModel
                {
                    Email = (UserManager.FindById(u.ToString())).Email,
                    Name = (UserManager.FindById(u.ToString())).FullName
                }).ToList());
        }
        [System.Web.Http.HttpPost, System.Web.Http.Route("addTeamToProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddTeamToProject(AddTeamToProjectModel model) {
            try {
                await _service.AddTeamToProjectAsync(model);
                return Ok("Ok");
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteTeamFromProject"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteTeamFromProject(DeleteTeamFromProjectModel model) {
            try {
                await _service.DeleteTeamFromProjectAsync(model);
                return Ok("Ok");
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [System.Web.Http.HttpPost, System.Web.Http.Route("addTeamToBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddTeamToBoard(AddTeamToBoardModel model) {
            try {
                await _service.AddTeamToBoardAsync(model);
                return Ok("Ok");
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteTeamFromBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteTeamFromBoard(DeleteTeamFromBoardModel model) {
            try {
                await _service.DeleteTeamFromBoardAsync(model);
                return Ok("Ok");
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

