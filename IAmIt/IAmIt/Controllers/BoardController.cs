﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using IAmIt.Models;
using IAmIt.Service.ProjectService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MongoDB.Bson;

namespace IAmIt.Controllers
{

    [System.Web.Http.Authorize]
    public class BoardController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IBoardService _service;

        public BoardController(IBoardService service)
        {
            _service = service;
        }


        [System.Web.Http.HttpPost, System.Web.Http.Route("addBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddBoard(AddBoardModel model)
        {
            model.UserId = new ObjectId(User.Identity.GetUserId());
            return Ok(await _service.AddBoardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteBoard(DeleteBoardModel model)
        {
            return Ok(await _service.DeleteBoardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeBoard(ChangeBoardModel model)
        {
            return Ok(await _service.ChangeBoardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("addUserToBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddUserToBoard(AddUserToBoardModel model)
        {
            model.UserId = new ObjectId((await UserManager.FindByEmailAsync(model.UserEmail)).Id);
            return Ok(await _service.AddUserToGroupBoardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteUserFromBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteUserFromBoard(DeleteUserFromBoardModel model)
        {
            model.UserId = new ObjectId((await UserManager.FindByEmailAsync(model.UserEmail)).Id);
            return Ok(await _service.DeleteUserFromBoardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteYourselfFromBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteYourselfFromBoard(DeleteUserFromBoardModel model)
        {
            model.UserId = new ObjectId(User.Identity.GetUserId());
            return Ok(await _service.DeleteYourselfFromBoardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getMyBoards"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetMyBoards(GetBoardModel model)
        {
            return Ok(await _service.GetMyBoardsAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getBoard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetBoard(GetBoardModel model)
        {
            return Ok(await _service.GetBoardAsync(model));
        }
    }
}
