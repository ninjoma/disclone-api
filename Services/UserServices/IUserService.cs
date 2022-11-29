using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.Services.UserServices
{
    public interface IUserService
    {
        Task<UserDTO> AddEditAsync(UserDTO user);

    }
}
