using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.Services.UserServices
{
    public interface IUserService
    {
        Task<UserGridDTO> GetById(int id, bool isActive = true);
        Task<List<UserGridDTO>> ListByName(string name, bool isActive = true);
        Task<UserDTO> AddEditAsync(UserDTO user);
        Task<UserDTO> ToggleInactiveById(int id);

    }
}
