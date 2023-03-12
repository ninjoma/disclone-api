using disclone_api.DTO;
using System.Security.Claims;

namespace disclone_api.Services
{
    public interface IAuthService
    {
        Task<UserDTO> GetUserByClaim(ClaimsPrincipal clp);
    }
}