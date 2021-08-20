using ElectronicSignage.Domain.Options;
using ElectronicSignage.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ElectronicSignage.Service
{
    /// <summary>
    /// 驗證服務
    /// </summary>
    public class AuthorizationService
    {
        private readonly TokenOptions tokenOptions;

        private readonly AuthorizationRepository authorizationRepository;

        public static readonly string AuthorizationTokenKey = "Authorization";

        public AuthorizationService(TokenOptions tokenOptions, AuthorizationRepository authorizationRepository)
        {
            this.tokenOptions = tokenOptions;
            this.authorizationRepository = authorizationRepository;
        }

        /// <summary>
        /// 驗證使用者帳號與密碼是否錯誤
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateCredentials(string account, string password)
        {
            var authorization = this.authorizationRepository.FetchAll(item => item.Account == account).Result.FirstOrDefault();
            if (authorization != null)
                return authorization.Password == password;

            return false;
        }

        /// <summary>
        /// 產生憑證
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public string GenerateToken(string account)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SymmetricKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                tokenOptions.AllowedIssuer,
                tokenOptions.AllowedAudience,
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, account)
                },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SymmetricKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParams = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                RequireExpirationTime = true,

                ValidateAudience = true,
                ValidAudience = tokenOptions.AllowedAudience,

                ValidateIssuer = true,
                ValidIssuer = tokenOptions.AllowedIssuer,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey
            };
            SecurityToken validatedToken;

            var validateResult = tokenHandler.ValidateToken(token.Replace("bearer ", ""), validationParams, out validatedToken);
            return validateResult;
        }
    }
}
