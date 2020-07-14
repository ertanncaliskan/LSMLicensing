using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseRegistrationAPI.Hubs
{
    public class SocketTokenRequirementHandler : AuthorizationHandler<SocketTokenRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;

        public SocketTokenRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SocketTokenRequirement requirement)
        {
            if (_httpContextAccessor.HttpContext.Request.Query["access_token"].ToString() == Startup.GeneralSettings.SocketSecret)
                context.Succeed(requirement);
            else
                context.Fail();
            return Task.CompletedTask;
        }
    }
    public class SocketTokenRequirement : IAuthorizationRequirement
    {

    }
}
