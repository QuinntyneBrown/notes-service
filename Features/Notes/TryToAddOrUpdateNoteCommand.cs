using MediatR;
using NotesService.Data;
using NotesService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace NotesService.Features.Notes
{
    public class TryToAddOrUpdateNoteCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public NoteApiModel Note { get; set; }
            public Guid CorrelationId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(NotesServiceContext context, IEventBus bus)
            {
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                var addOrUpdateRequest = new AddOrUpdateNoteCommand.Request();
                addOrUpdateRequest.Note = request.Note;
                addOrUpdateRequest.CorrelationId = request.CorrelationId;
                addOrUpdateRequest.Username = request.Username;
                _bus.Publish(new TryToAddOrUpdateNoteMessage(addOrUpdateRequest, request.CorrelationId, request.TenantUniqueId, request.Username));

                return new Response();
            }
            
            private IEventBus _bus;
        }

    }
}
