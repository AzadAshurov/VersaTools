using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TokenResponseDto> CreateToken(AppUser user, int minutes)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));


            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var keyString = _configuration["JWT:Key"];


            if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
            {
                _logger.LogError("JWT:Issuer or JWT:Audience is missing in configuration.");
                throw new InvalidOperationException("JWT:Issuer or JWT:Audience is missing in configuration.");
            }

            if (string.IsNullOrWhiteSpace(keyString))
            {
                _logger.LogError("JWT:Key is missing in configuration.");
                throw new InvalidOperationException("JWT:Key is missing in configuration.");
            }


            var rawKey = Encoding.UTF8.GetBytes(keyString);
            if (rawKey.Length < 32)
            {
                _logger.LogError("JWT:Key must be at least 32 characters to meet 256-bit security requirement.");
                throw new InvalidOperationException("JWT:Key is too short. Must be at least 256 bits (32 bytes).");
            }


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


            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(rawKey);


            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                securityKey,
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256
            );


            var securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                notBefore: DateTime.UtcNow,
                claims: userClaims,
                signingCredentials: signingCredentials
            );


            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(securityToken);


            _logger.LogInformation("Generated JWT for user {UserName}, Expires at: {Expires}",
                                   user.UserName, securityToken.ValidTo);

            return new TokenResponseDto(tokenString, user.UserName, securityToken.ValidTo);
        }
    }
}
