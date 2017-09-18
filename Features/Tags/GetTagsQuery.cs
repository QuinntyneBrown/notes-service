using MediatR;
using NotesService.Data;
using NotesService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace NotesService.Features.Tags
{
    public class GetTagsQuery
    {
        public class Request : BaseRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<TagApiModel> Tags { get; set; } = new HashSet<TagApiModel>();
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
                var tags = await _context.Tags
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new Response()
                {
                    Tags = tags.Select(x => TagApiModel.FromTag(x)).ToList()
                };
            }

            private readonly NotesServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
