using ElectronicSignage.Domain;
using ElectronicSignage.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ElectronicSignage.Web.Api
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "PermissionHandler")]
    public class AuthenticationController : Controller
    {
        private readonly AuthorizationService authorizationService;

        public AuthenticationController(AuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GenerateToken([FromBody] dynamic userInformation)
        {
            var validResult = new ValidResult();

            var account = userInformation.account != null ? (string)userInformation.account : string.Empty;
            var password = userInformation.password != null ? (string)userInformation.password : string.Empty;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                validResult.ErrorMessages.Add(Guid.NewGuid().ToString(), "使用者名稱或密碼不能為空白");
                return StatusCode(StatusCodes.Status400BadRequest, validResult);
            }

            var validateCredentialsResult = this.authorizationService.ValidateCredentials(account, password);

            if (!validateCredentialsResult)
            {
                validResult.ErrorMessages.Add(Guid.NewGuid().ToString(), "使用者名稱或密碼錯誤");
                return StatusCode(StatusCodes.Status400BadRequest, validResult);
            }

            var token = this.authorizationService.GenerateToken(account);
            Response.Cookies.Append("Authorization", token);

            var result = new { token = token };
            return Ok(result);
        }

        [HttpPost]
        public ActionResult CanActivate([FromBody] dynamic functionKeys)
        {
            return Ok(true);
        }
    }
}
