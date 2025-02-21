using VersaTools.Application.DTOs.Tokens;
using VersaTools.Domain.Entitities;

namespace VersaTools.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateToken(AppUser user, int minutes);
    }
}
