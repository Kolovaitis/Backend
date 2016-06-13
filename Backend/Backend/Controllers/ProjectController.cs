using Backend.Models.ProjectModels;
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

        [HttpGet, Route("addProject")]
        public async Task<IHttpActionResult> AddProject(ProjectOnlyNameModel model)
        {
            //_service.AddProject(await UserManager.FindByIdAsync(User.Identity.GetUserId()), model.Name);
            return Ok();
        }
    }
}
