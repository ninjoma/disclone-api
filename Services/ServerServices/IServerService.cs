
using disclone_api.DTOs.ServerDTOs;

namespace disclone_api.Services.ServerServices
{
    public interface IServerService
    {
        Task<ServerDTO> GetById(int id, bool isActive = true);
        Task<List<ServerDTO>> ListByName(string name, bool isActive = true);
        Task<ServerDTO> AddEditAsync(ServerDTO server);
        Task<ServerDTO> ToggleInactiveById(int id);
    }
}
