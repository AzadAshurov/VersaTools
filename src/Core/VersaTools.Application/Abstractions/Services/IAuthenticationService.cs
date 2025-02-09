using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.DTOs.AppUsers;
using VersaTools.Application.DTOs.Tokens;

namespace VersaTools.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDto userDto);
        Task<TokenResponseDto> LoginAsync(LoginDto userDto);
        Task LogoutAsync(string userId);
    
    }
}
