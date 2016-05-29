using Backend.Models;
using Backend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    { 
        private IService _service;
        public HomeController(IService service)
        {
            _service = service;
        }

        [HttpPost, Route("getUser")]
        public IHttpActionResult GetUser(UserOnlyEmailModel user)
        {
            try {
                return Ok(_service.GetUserByEmail(user));
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("registration"), AllowAnonymous]
        public async Task<IHttpActionResult> Registration(UserRegistrationModel user)
        {
            try {
                await _service.Registration(user);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpPost, Route("changeInfo")]
        public IHttpActionResult ChangeInfo(UserChangeInfoModel user)
        {
            _service.ChangeInfo(user);
            return Ok();
        }

        [HttpPost, Route("changeCredentials")]
        public async Task<IHttpActionResult> ChangeCredentials(UserChangeCredentialsModel user)
        {
            try {
                await _service.ChangeCredentials(user);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
