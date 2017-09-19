using MediatR;
using NotesService.Data;
using NotesService.Features.Core;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace NotesService.Features.Tenants
{
    public class TenantExistsResponse
    {
        public bool Exists { get; set; }
    }

    public class SetTenantCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public Guid UniqueId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(NotesServiceContext context, HttpClient client)
            {
                _context = context;
                _client = client;
            }

            public async Task<Response> Handle(Request request)
            {
                var tenant = await _context.Tenants.SingleOrDefaultAsync(x => x.UniqueId == request.TenantUniqueId);

                if (tenant != null)
                    return new Response();

                var httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"http://identity.quinntynebrown.com/api/tenants/exists?uniqueId={request.UniqueId}"),
                    Method = HttpMethod.Get,
                };

                httpRequestMessage.Headers.Add("Tenant", $"{request.UniqueId}");
                
                var httpResponseMessage = await _client.SendAsync(httpRequestMessage);
                var response = await httpResponseMessage.Content.ReadAsAsync<JObject>();
                
                if(Convert.ToBoolean(response["exists"]))
                {
                    tenant = new Model.Tenant() { UniqueId = request.UniqueId, Name = $"{request.UniqueId}" };
                    _context.Tenants.Add(tenant);
                    await _context.SaveChangesAsync();
                    return new Response();
                }

                throw new Exception("Tenant doesn't exist");
            }

            private readonly NotesServiceContext _context;            
            private readonly HttpClient _client;
        }
    }
}
