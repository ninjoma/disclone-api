using disclone_api.DTO;
using disclone_api.Entities;

namespace disclone_api.Services
{
    public interface IUserService : IMainService<UserDTO, UserDetailDTO>
    {
        Task<List<UserDTO>> ListByName(string name, bool isActive = true);
        Task<UserDTO> Register(UserDTO user);
    }
}
