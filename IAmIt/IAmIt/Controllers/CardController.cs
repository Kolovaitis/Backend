using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using IAmIt.Models;
using IAmIt.Service.BoardService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MongoDB.Bson;
using IAmIt.Service.CardService;

namespace IAmIt.Controllers
{
    [System.Web.Http.Authorize]
    public class CardController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }


        [System.Web.Http.HttpPost, System.Web.Http.Route("addCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddCard(AddCardModel model)
        {
            return Ok(await _service.AddCardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteCard(DeleteCardModel model)
        {
            await _service.DeleteCardAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeCard(ChangeCardModel model)
        {
            await _service.ChangeCardAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("addUserToCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddUserToCard(AddUserToCardModel model)
        {
            try
            {
                model.UserId = new ObjectId((await UserManager.FindByEmailAsync(model.UserEmail)).Id);
                await _service.AddUserToCardAsync(model);
                return Ok("Ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteUserFromCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteUserFromCard(DeleteUserFromCardModel model)
        {
            model.UserId = new ObjectId((await UserManager.FindByEmailAsync(model.UserEmail)).Id);
            await _service.DeleteUserFromCardAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteYourselfFromCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteYourselfFromCard(DeleteYourselfFromCardModel model)
        {
            model.UserId = new ObjectId(User.Identity.GetUserId());
            await _service.DeleteYourselfFromCardAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("getMyCards"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetMyCards()
        {
            return Ok(await _service.GetMyCardsAsync(new ObjectId(User.Identity.GetUserId())));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetCard(GetCardModel model)
        {
            return Ok(await _service.GetCardAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("getUsersInCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> GetUsersInCard(GetUsersInCardModel model)
        {
            return Ok((await _service.GetUsersInCardAsync(model)).
                Select(u => new UserToSendModel
                {
                    Email = (UserManager.FindById(u.ToString())).Email,
                    Name = (UserManager.FindById(u.ToString())).FullName
                }).ToList());
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("moveCardInOtherColumn"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> MoveCardInOtherColumn(MoveCardInOtherColumnModel model)
        {
            await _service.MoveCardInOtherColumnAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("moveCard"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> MoveCard(MoveCardModel model)
        {
            await _service.MoveCardAsync(model);
            return Ok("Ok");
        }
    }
}
