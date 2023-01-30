
using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IServerService : IMainService<ServerDTO>
    {
        Task<List<ServerDTO>> ListByName(string name, bool isActive = true);
    }
}
