using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.Tokens;
using VersaTools.Domain.Entitities;

namespace VersaTools.Infrastructure.Implementations.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<TokenHandler> _logger;

        public TokenHandler(IConfiguration configuration,
                            UserManager<AppUser> userManager,
                            ILogger<TokenHandler> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        public TokenResponseDto CreateToken(AppUser user, int minutes)
        {
            IEnumerable<Claim> userClaims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Name, user.UserName),
    new Claim(ClaimTypes.GivenName, user.Name),
    new Claim(ClaimTypes.Surname, user.Surname)
};

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["JWT:Key"])
            );

            SigningCredentials credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                notBefore: DateTime.Now,
                claims: userClaims,
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(securityToken);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(securityToken), user.UserName, securityToken.ValidTo);
        }
    }
}
