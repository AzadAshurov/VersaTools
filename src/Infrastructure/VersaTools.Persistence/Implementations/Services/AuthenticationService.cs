using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.IdentityDTO;
using VersaTools.Domain.Entitities.Identity;

namespace VersaTools.Persistence.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            UserManager<User> userManager,
             SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
            var user = new User 
            { 
                UserName = model.Email, 
                Email = model.Email,
                // Add any additional user properties here
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                // Optionally assign a default role
                await _userManager.AddToRoleAsync(user, "User");
                
                // Optionally generate email confirmation token
                // var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            
            return result;
        }

        public async Task<string?> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            // Optionally check email confirmation  ftg
            // if (!await _userManager.IsEmailConfirmedAsync(user)) return null;

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true); // Enable lockout
            if (!result.Succeeded) return null;

            return await GenerateJwtToken(user);
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add user roles to claims
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var expires = DateTime.UtcNow.AddMinutes(
                Convert.ToInt32(_configuration["Jwt:ExpirationInMinutes"] ?? "60"));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
