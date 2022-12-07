using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.Services.UserServices
{
    public interface IUserService
    {
        Task<UserDTO> GetById(int id, bool isActive = true);
        Task<List<UserDTO>> ListByName(string name, bool isActive = true);
        Task<UserDTO> AddEditAsync(UserDTO user);
        Task<UserDTO> ToggleInactiveById(int id);

    }
}
