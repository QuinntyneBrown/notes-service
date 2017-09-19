using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NotesService.Features.Core;

namespace NotesService.Features.Tenants
{
    [Authorize]
    [RoutePrefix("api/tenants")]
    public class TenantController : BaseApiController
    {
        public TenantController(IMediator mediator)
            :base(mediator) { }

        [Route("set")]
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(SetTenantCommand.Response))]
        public async Task<IHttpActionResult> Set(SetTenantCommand.Request request) => Ok(await Send(request));
    }
}
