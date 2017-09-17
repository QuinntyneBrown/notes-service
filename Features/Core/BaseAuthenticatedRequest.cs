using System;
using System.Linq;
using System.Security.Claims;

namespace NotesService.Features.Core
{
    public class BaseAuthenticatedRequest: BaseRequest
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        //public new Guid TenantUniqueId
        //{
        //    get
        //    {
        //        return new Guid(ClaimsPrincipal?.Claims.Single(x => x.Type == Core.ClaimTypes.TenantUniqueId).Value);
        //    }
        //}

        private string _username;

        public string Username
        {
            get {
                if(string.IsNullOrEmpty(_username))
                    return ClaimsPrincipal?.Identity.Name;

                return _username;
            }  

            set { _username = value; }
        }

        
    }
}