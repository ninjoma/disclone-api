
using disclone_api.DTOs.ServerDTOs;

namespace disclone_api.Services.ServerServices
{
    public interface IServerService
    {
        // TODO: Implementar la busqueda por inactivos
        Task<ServerDTO> GetById(int id);
        Task<List<ServerDTO>> ListByName(string name);
        Task<ServerDTO> AddEditAsync(ServerDTO server);
        Task<ServerDTO> ToggleInactiveById(int id);
    }
}
