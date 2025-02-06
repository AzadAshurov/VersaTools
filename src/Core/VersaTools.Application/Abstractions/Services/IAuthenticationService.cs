using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VersaTools.Application.DTOs.IdentityDTO;

namespace VersaTools.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO model);
        Task<string?> LoginAsync(LoginDTO model);
    }
}
