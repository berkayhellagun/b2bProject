using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;
        TokenOptions _tokenOptions;
        DateTime _expirationTime;
        public JwtHelper(IConfiguration configuration)
        {
            // we need to read appSettings.json file so i used configuration interface
            _configuration = configuration;
            // "Get<T>" method came over here from "binder" nuget pack
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _expirationTime = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpirationTime);
        }
        public Task<AccessToken> AsyncCreateToken(User user, List<OperationClaim> operationClaims)
        {
            return Task.FromResult(CreateToken(user, operationClaims));
        }

        private AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            // security key 
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            // signing credentials
            var signingCredentials = SigningCredentialsHelper.CreateCredentials(securityKey);
            //jwt create
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            // token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenName = tokenHandler.WriteToken(jwt);
            // return access token 
            return new AccessToken { Token = tokenName, ExpirationTime = _expirationTime };
        }
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, 
                                                        SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: _expirationTime,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
                );
            return jwt;
        }
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.UserId.ToString());
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(o => o.OperationName).ToArray());
            return claims;
        }
    }
}
