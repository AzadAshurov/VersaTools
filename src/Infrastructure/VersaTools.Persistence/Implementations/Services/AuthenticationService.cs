using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.AppUsers;
using VersaTools.Application.DTOs.Tokens;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
       
        private readonly ITokenHandler _handler;
        //ctrl+r+g
        public AuthenticationService(
            UserManager<AppUser> userManager,
           
            ITokenHandler handler

            )
        {
            _userManager = userManager;
         
            _handler = handler;
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
 
            var result = await _userManager.CreateAsync(new AppUser
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                UserName = userDto.UserName,
                Email = userDto.Email,
                IsActive = true
            }, userDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder str = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    str.AppendLine(error.Description);
                }
                throw new Exception(str.ToString());
            }

        }
    }
}
