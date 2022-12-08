using disclone_api.DTOs.UserDTOs;
using System.Security.Claims;

namespace disclone_api.Services.AuthServices
{
    public interface IAuthService
    {
        Task<UserDTO> GetUserByClaim(ClaimsPrincipal clp);
    }
}