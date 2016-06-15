﻿using Backend.Models.ProjectModels;
using Backend.Service;
using Backend.Service.ProjectService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Controllers
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

        [HttpPost, Route("addProject")]
        public async Task<IHttpActionResult> AddProject(AddProjectModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            return Ok(await _service.AddProject(model));
        }

        [HttpGet, Route("getMyProjects")]
        public async Task<IHttpActionResult> GetMyProjects()
        {
            return Ok(await _service.getMyProjects((await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email));
        }

        [HttpPost, Route("changeProject")]
        public async Task<IHttpActionResult> ChangeProject(ChangeProjectModel model)
        {
            await _service.ChangeProject(model);
            return Ok();
        }

        [HttpPost, Route("deleteProject")]
        public async Task<IHttpActionResult> DeleteProject(DeleteProjectModel model)
        {
            await _service.DeleteProject(model);
            return Ok();
        }

        [HttpPost, Route("inviteUserToProject")]
        public async Task<IHttpActionResult> InviteUserToProject(InviteUserToProjectModel model)
        {
            //model.EmailSender = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            await _service.InviteUserToProject(model);
            return Ok();
        }

        [HttpPost, Route("acceptInvitation")]
        public async Task<IHttpActionResult> AcceptInvitation(AcceptInvitationModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            await _service.AcceptInvitation(model);
            return Ok();
        }

        [HttpPost, Route("rejectInvitation")]
        public async Task<IHttpActionResult> RejectInvitation(RejectInvitationModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            await _service.RejectInvitation(model);
            return Ok();
        }

        [HttpGet, Route("getAllInvitations")]
        public async Task<IHttpActionResult> GetAllInvitations()
        {
            return Ok(_service.GetAllInvitations((await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email));
        }

        [HttpPost, Route("deleteUserFromProject")]
        public async Task<IHttpActionResult> DeleteUserFromProject(DeleteUserFromProjectModel model)
        {
            await DeleteUserFromProject(model);
            return Ok();
        }

        [HttpPost, Route("getProject")]
        public async Task<IHttpActionResult> GetProject(GetProjectModel model)
        {
            return Ok(await _service.GetProject(model));
        }
    }
}
