using Backend.Abstractions;
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

        [HttpGet, Route("hello")]
        public IHttpActionResult Hello()
        {
            return Ok("hello");
        }

        [HttpPost, Route("registration"), AllowAnonymous]
        public IHttpActionResult Registration(User user)
        {
            _service.Registration(user);
            return Ok();
        }

        [HttpPost, Route("name"), AllowAnonymous]
        public IHttpActionResult Name()
        {
            
            return Ok(_service.GetUserByEmail("egorix2000@mail.ru"));
        }
    }
}
