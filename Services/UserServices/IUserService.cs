using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.Services.UserServices
{
    public interface IUserService
    {
        Task<UserGridDTO> GetById(int id, bool isActive = true);
        Task<List<UserGridDTO>> ListByName(string name, bool isActive = true);
        Task<UserDTO> AddEdit(UserDTO user);
        Task<UserDTO> Delete(int id);
        Task<UserDTO> Register(UserDTO user);
    }
}
