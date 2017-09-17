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
            public Handler(NotesServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                throw new System.NotImplementedException();
            }

            private readonly NotesServiceContext _context;
            private readonly ICache _cache;
        }

    }
}
