using ElectronicSignage.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ElectronicSignage.Web.Handler
{
    public class PermissionHandler : AuthenticationHandler<PermissionHandlerOptions>
    {
        private readonly AuthorizationService authorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PermissionHandler(
            AuthorizationService authorizationService,
            IOptionsMonitor<PermissionHandlerOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, 
            ISystemClock clock,
            IHttpContextAccessor httpContextAccessor)
            : base(options, logger, encoder, clock)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.authorizationService = authorizationService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var request = httpContextAccessor.HttpContext.Request;
            
            string token;
            request.Cookies.TryGetValue(AuthorizationService.AuthorizationTokenKey, out token);

            if (string.IsNullOrEmpty(token))
            {
                request.Headers.TryGetValue(AuthorizationService.AuthorizationTokenKey, out StringValues stringValues);
                if (stringValues.Count > 0)
                    token = stringValues[0];
            }

            if (string.IsNullOrEmpty(token))
                return Task.FromResult(AuthenticateResult.Fail(""));

            try
            {
                var principal = authorizationService.ValidateToken(token);
                if (principal != null)
                {
                    var authenticationTicket = new AuthenticationTicket(principal, "AuthenticationScheme");
                    return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
                }
            }
            catch
            {
            }

            return Task.FromResult(AuthenticateResult.Fail(""));
        }
    }
}
