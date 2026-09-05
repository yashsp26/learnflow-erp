using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LearnFlowERP.Infrastructure.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSecurityTokenHandler _handler;
        private readonly SigningCredentials _credentials;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtTokenService(IConfiguration config)
        {
            _handler = new JwtSecurityTokenHandler();

            var jwtKey = config["Jwt:Key"];
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException(
                    "JWT configuration 'Jwt:Key' is missing.");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new InvalidOperationException(
                    "JWT configuration 'Jwt:Issuer' is missing.");

            if (string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException(
                    "JWT configuration 'Jwt:Audience' is missing.");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            _credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            _issuer = issuer;
            _audience = audience;

        }


        public string GenerateToken(User user, long roleId)
        {
            var claims = new List<Claim>
        {
            new("UserId", user.UserId.ToString()),
            new("TenantId", user.TenantId.ToString()),
            new("UserType", user.UserType.ToString()),
            new("RoleId", roleId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: _credentials);

            return _handler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }
    }
}
