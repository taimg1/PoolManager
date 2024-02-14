using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PoolMS.Core.Entities;
using PoolMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.JWT
{
    public  sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string Generate(User user)
        {
            var claims = new Claim[] 
            {
               new("email", user.Email),
               new("role", user.Role.ToString())
            };
            var signingKey = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               _jwtOptions.Issuer,
               _jwtOptions.Audience,
               claims,
               null,
               DateTime.UtcNow.AddHours(1),
               signingKey);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
