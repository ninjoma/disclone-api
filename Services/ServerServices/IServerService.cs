
using disclone_api.DTOs.ServerDTOs;
using disclone_api.DTOs.UserDTOs;

namespace disclone_api.Services.ServerServices
{
    public interface IServerService
    {
        Task<ServerGridDTO> GetById(int id, bool isActive = true);
        Task<List<ServerGridDTO>> ListByName(string name, bool isActive = true);
        Task<ServerDTO> Add(ServerDTO server);
        Task<ServerDTO> EditById(ServerDTO server);
        Task<ServerDTO> Delete(int id);
    }
}
