using ElectronicSignage.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ElectronicSignage.Web.Handler
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly AuthorizationService authorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PermissionRequirementHandler(
            AuthorizationService authorizationService, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}
