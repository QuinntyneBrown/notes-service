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
    public class GetByTitleAndCurrentUserQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> {
            public string Title { get; set; }
        }

        public class Response
        {
            public NoteApiModel Note { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(NotesServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var note = await _context.Notes.Where(
                    x => x.Title == request.Title
                    && x.CreatedBy == request.Username
                    && x.Tenant.UniqueId == request.TenantUniqueId).SingleOrDefaultAsync();

                if (note == null) return new Response();

                return new Response()
                {
                    Note = NoteApiModel.FromNote(note)
                };
            }

            private readonly NotesServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
