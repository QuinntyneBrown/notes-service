using MediatR;
using NotesService.Data;
using NotesService.Model;
using NotesService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace NotesService.Features.Tags
{
    public class RemoveTagCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public int Id { get; set; }
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
                var tag = await _context.Tags.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                tag.IsDeleted = true;
                await _context.SaveChangesAsync();
                _bus.Publish(new RemovedTagMessage(request.Id, request.CorrelationId, request.TenantUniqueId));
                return new Response();
            }

            private readonly NotesServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
