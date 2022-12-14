
using disclone_api.DTOs.ServerDTOs;

namespace disclone_api.Services.ServerServices
{
    public interface IServerService
    {
        Task<ServerGridDTO> GetById(int id, bool isActive = true);
        Task<List<ServerGridDTO>> ListByName(string name, bool isActive = true);
        Task<ServerDTO> AddEditAsync(ServerDTO server);
        Task<ServerDTO> ToggleInactiveById(int id);
    }
}
