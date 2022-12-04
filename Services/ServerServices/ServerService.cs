using disclone_api.DTOs.ServerDTOs;

namespace disclone_api.Services.ServerServices
{
    public class ServerService : IServerService
    {
        public Task<ServerDTO> AddEditAsync(ServerDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<ServerDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ServerDTO>> ListByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServerDTO> ToggleInactiveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
