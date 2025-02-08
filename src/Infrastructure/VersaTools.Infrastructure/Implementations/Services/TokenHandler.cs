using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.Tokens;
using VersaTools.Domain.Entitities;

namespace VersaTools.Infrastructure.Implementations.Services
{
    internal class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponseDto CreateToken(AppUser user, int minutes)
        {
            IEnumerable<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"])
                );

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires: DateTime.Now.AddMinutes(minutes),
                notBefore: DateTime.Now,
                claims: userClaims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(securityToken), user.UserName, securityToken.ValidTo);
        }
    }
}
