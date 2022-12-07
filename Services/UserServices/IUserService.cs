using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;

namespace disclone_api.Services.UserServices
{
    public interface IUserService
    {
        // TODO: Implementar la busqueda por inactivos
        Task<UserDTO> GetById(int id);
        Task<List<UserDTO>> ListByName(string name);
        Task<UserDTO> AddEditAsync(UserDTO user);
        Task<UserDTO> ToggleInactiveById(int id);

    }
}
