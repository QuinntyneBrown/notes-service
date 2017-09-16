using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NotesService.Features.Core;

namespace NotesService.Features.Notes
{
    [Authorize]
    [RoutePrefix("api/notes")]
    public class NoteController : BaseApiController
    {
        public NoteController(IMediator mediator)
            :base(mediator) { }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateNoteCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateNoteCommand.Request request) => Ok(await Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateNoteCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateNoteCommand.Request request) => Ok(await Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetNotesQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await Send(new GetNotesQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetNoteByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetNoteByIdQuery.Request request) => Ok(await Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveNoteCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveNoteCommand.Request request) => Ok(await Send(request));

    }
}
