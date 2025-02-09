using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager; 

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager; 
        }

        public async Task<TokenResponseDto> CreateToken(AppUser user, int minutes)
        {
            var roles = await _userManager.GetRolesAsync(user); 

            var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname)
        };

            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role)); 
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"])
            );

            var securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires: DateTime.UtcNow.AddHours(minutes * 100),
                notBefore: DateTime.Now,
                claims: userClaims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(securityToken), user.UserName, securityToken.ValidTo);
        }
    }

}
