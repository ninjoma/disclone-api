
using disclone_api.DTOs.ServerDTOs;

namespace disclone_api.Services.ServerServices
{
    public interface IServerService
    {
        Task<ServerDTO> GetById(int id);
        Task<List<ServerDTO>> ListByName(string name);
        Task<ServerDTO> AddEditAsync(ServerDTO user);
        Task<ServerDTO> ToggleInactiveById(int id);
    }
}
