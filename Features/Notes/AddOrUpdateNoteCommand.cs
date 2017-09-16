using MediatR;
using NotesService.Data;
using NotesService.Model;
using NotesService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace NotesService.Features.Notes
{
    public class AddOrUpdateNoteCommand
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
                _context = context;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.Notes
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Note.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    var existingNote = await _context.Notes.SingleOrDefaultAsync(x => x.Title == request.Note.Title && x.CreatedBy == request.Username);

                    if (existingNote != null) throw new Exception("Note Exists");

                    _context.Notes.Add(entity = new Note() { TenantId = tenant.Id });
                }

                entity.Title = request.Note.Title;

                entity.Body = request.Note.Body;

                entity.Slug = request.Note.Title.GenerateSlug();

                await _context.SaveChangesAsync(request.Username);

                _bus.Publish(new AddedOrUpdatedNoteMessage(entity, request.CorrelationId, request.TenantUniqueId));

                return new Response();
            }

            private readonly NotesServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
