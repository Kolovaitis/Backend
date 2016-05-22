﻿using Backend.Abstractions;
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
        public IHttpActionResult GetUser(User user)
        {
            return Ok(_service.GetUserByEmail(user));
        }

        [HttpPost, Route("registration"), AllowAnonymous]
        public IHttpActionResult Registration(User user)
        {
            _service.Registration(user);
            return Ok();
        }

        [HttpPost, Route("changeInfo")]
        public IHttpActionResult ChangeInfo(User user)
        {
            _service.ChangeInfo(user);
            return Ok();
        }

        [HttpPost, Route("changeCredentials")]
        public IHttpActionResult ChangeCredentials(User user)
        {
            _service.ChangeCredentials(user);
            return Ok();
        }
    }
}
