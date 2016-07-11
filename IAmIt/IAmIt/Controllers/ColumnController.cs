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
using IAmIt.Service.ColumnService;

namespace IAmIt.Controllers
{
    [System.Web.Http.Authorize]
    public class ColumnController : ApiController
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IColumnService _service;
        
        public ColumnController(IColumnService service)
        {
            _service = service;
        }


        [System.Web.Http.HttpPost, System.Web.Http.Route("addColumn"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> AddColumn(AddColumnModel model)
        {
            return Ok(await _service.AddColumnAsync(model));
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("deleteColumn"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> DeleteColumn(DeleteColumnModel model)
        {
            await _service.DeleteColumnAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("changeColumn"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> ChangeColumn(ChangeColumnModel model)
        {
            await _service.ChangeColumnAsync(model);
            return Ok("Ok");
        }

        [System.Web.Http.HttpPost, System.Web.Http.Route("moveColumn"), ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> MoveColumn(MoveColumnModel model)
        {
            await _service.MoveColumnAsync(model);
            return Ok("Ok");
        }
    }
}
