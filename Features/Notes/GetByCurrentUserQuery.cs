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
    public class GetByCurrentUserQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<NoteApiModel> Notes { get; set; } = new HashSet<NoteApiModel>();
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
                var notes = await _context.Notes.Where(x =>
                x.CreatedBy == request.Username
                && x.Tenant.UniqueId == request.TenantUniqueId).ToListAsync();

                return new Response()
                {
                    Notes = notes.Select(x => NoteApiModel.FromNote(x)).ToList()
                };
            }

            private readonly NotesServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
