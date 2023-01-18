
using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IServerService
    {
        Task<ServerGridDTO> GetById(int id, bool isActive = true);
        Task<List<ServerGridDTO>> ListByName(string name, bool isActive = true);
        Task<ServerDTO> AddEdit(ServerDTO server);
        Task<ServerDTO> Delete(int id);
    }
}
