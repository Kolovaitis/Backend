﻿using Backend.Models.ProjectModels;
using Backend.Service;
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

        private readonly IService _service;

        public ProjectController(IService service)
        {
            _service = service;
        }

        [HttpPost, Route("addProject")]
        public async Task<IHttpActionResult> AddProject(AddProjectModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            return Ok(/*_service.AddProject(model);*/);
        }

        [HttpGet, Route("getMyProjects")]
        public async Task<IHttpActionResult> GetMyProjects()
        {
            return Ok(/*_service.getMyProjects((await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email);*/);
        }

        [HttpPost, Route("changeProject")]
        public async Task<IHttpActionResult> ChangeProject(ChangeProjectModel model)
        {
            /*_service.ChangeProject(model);*/
            return Ok();
        }

        [HttpPost, Route("deleteProject")]
        public async Task<IHttpActionResult> DeleteProject(DeleteProjectModel model)
        {
            /*_service.DeleteProject(model);*/
            return Ok();
        }

        [HttpPost, Route("inviteUserToProject")]
        public async Task<IHttpActionResult> InviteUserToProject(InviteUserToProjectModel model)
        {
            //model.EmailSender = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            /*_service.InviteUserToProject(model);*/
            return Ok();
        }

        [HttpPost, Route("acceptInvitation")]
        public async Task<IHttpActionResult> AcceptInvitation(AcceptInvitationModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            /*_service.AcceptInvitation(model);*/
            return Ok();
        }

        [HttpPost, Route("rejectInvitation")]
        public async Task<IHttpActionResult> RejectInvitation(RejectInvitationModel model)
        {
            model.UserEmail = (await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email;
            /*_service.RejectInvitation(model);*/
            return Ok();
        }

        [HttpGet, Route("getAllInvitations")]
        public async Task<IHttpActionResult> GetAllInvitations()
        {
            return Ok(/*_service.GetAllInvitations((await UserManager.FindByIdAsync(User.Identity.GetUserId())).Email);*/);
        }

        [HttpPost, Route("deleteUserFromProject")]
        public async Task<IHttpActionResult> DeleteUserFromProject(DeleteUserFromProjectModel model)
        {
            /*_service.DeleteUserFromProject(model);*/
            return Ok();
        }

        [HttpPost, Route("getProject")]
        public async Task<IHttpActionResult> GetProject(GetProjectModel model)
        {
            return Ok(/*_service.DeleteUserFromProject(model);*/);
        }
    }
}
