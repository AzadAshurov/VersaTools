using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.AppUsers;
using VersaTools.Application.DTOs.Tokens;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ITokenHandler _handler;



        public AuthenticationService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenHandler handler
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _handler = handler;
        }


        private async Task EnsureRolesExist()
        {
            string[] roles = { "MainAdmin", "Moderator", "Member" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public async Task LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");


        }
        public async Task<TokenResponseDto> LoginAsync(LoginDto userDto)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserNameOrEmail || u.Email == userDto.UserNameOrEmail);

            if (user == null)
                throw new Exception("Username, Email or Password is incorrect");


            bool result = await _userManager.CheckPasswordAsync(user, userDto.Password);

            if (!result)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("Username, Email or Password is incorrect");
            }
            return _handler.CreateToken(user, 15);

        }
        public async Task RegisterAsync(RegisterDto userDto)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == userDto.UserName || u.Email == userDto.Email))
                throw new Exception("User already exist");

            await EnsureRolesExist();

            var user = new AppUser
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                UserName = userDto.UserName,
                Email = userDto.Email,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder str = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    str.AppendLine(error.Description);
                }
                throw new Exception(str.ToString());
            }


            await _userManager.AddToRoleAsync(user, "Member");
        }


    }
}

