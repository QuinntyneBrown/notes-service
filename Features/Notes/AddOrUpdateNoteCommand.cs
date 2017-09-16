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
        public class Request : BaseRequest, IRequest<Response>
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
                    _context.Notes.Add(entity = new Note() { TenantId = tenant.Id });
                }

                entity.Title = request.Note.Title;

                entity.Body = request.Note.Body;

                await _context.SaveChangesAsync();

                _bus.Publish(new AddedOrUpdatedNoteMessage(entity, request.CorrelationId, request.TenantUniqueId));

                return new Response();
            }

            private readonly NotesServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}