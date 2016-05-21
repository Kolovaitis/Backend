using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Controllers
{
    public class MyController : ApiController
    {

        [HttpGet, Route("hello")]
        public IHttpActionResult Hello()
        {
            return Ok("hello");
        }
    }
}
