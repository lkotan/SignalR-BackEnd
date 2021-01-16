using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Chat.Core.Extensions;
using Chat.Core.Helpers;

namespace Chat.Core.Plugins.Authentication.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private readonly JwtOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(JwtOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }
        
        public AccessToken CreateToken(int accountId)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(_tokenOptions, accountId, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                RefreshToken = Helper.CreateToken(),
                TokenExpiration = _accessTokenExpiration
            };
        }
        private JwtSecurityToken CreateJwtSecurityToken(JwtOptions tokenOptions, int accountId, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(accountId),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private static IEnumerable<Claim> SetClaims(int accountId)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(accountId.ToString());
            return claims;
        }
    }
}
