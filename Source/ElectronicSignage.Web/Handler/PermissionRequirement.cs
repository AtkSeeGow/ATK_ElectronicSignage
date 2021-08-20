using Microsoft.AspNetCore.Authorization;

namespace ElectronicSignage.Web.Handler
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement()
        {
        }
    }
}
