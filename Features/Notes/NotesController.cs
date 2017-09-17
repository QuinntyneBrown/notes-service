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
        public NoteController(IMediator mediator, IEventBus bus)
            :base(mediator) {
            _bus = bus;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult Add(AddOrUpdateNoteCommand.Request request) {
            _bus.Publish(new TryToAddOrUpdateNoteMessage(request, request.CorrelationId, TenantUniqueId, User.Identity.Name));
            return Ok();
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateNoteCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateNoteCommand.Request request) => Ok(await Send(request));

        [Route("get")]
        [HttpGet]
        [ResponseType(typeof(GetNotesQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await Send(new GetNotesQuery.Request()));

        [Route("getBySlugAndCurrentUser")]
        [HttpGet]
        [ResponseType(typeof(GetBySlugAndCurrentUserQuery.Response))]
        public async Task<IHttpActionResult> GetBySlugAndCurrentUser([FromUri]GetBySlugAndCurrentUserQuery.Request request) => Ok(await Send(request));

        [Route("getByTitleAndCurrentUser")]
        [HttpGet]
        [ResponseType(typeof(GetByTitleAndCurrentUserQuery.Response))]
        public async Task<IHttpActionResult> GetByTitleAndCurrentUserQuery([FromUri]GetByTitleAndCurrentUserQuery.Request request) => Ok(await Send(request));

        [Route("getByCurrentUser")]
        [HttpGet]
        [ResponseType(typeof(GetByCurrentUserQuery.Response))]
        public async Task<IHttpActionResult> GetByCurrentUserQuery() => Ok(await Send(new GetByCurrentUserQuery.Request()));


        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetNoteByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetNoteByIdQuery.Request request) => Ok(await Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveNoteCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveNoteCommand.Request request) => Ok(await Send(request));

        private IEventBus _bus { get; set; }
    }
}
