using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IChannelService
    {
        public Task<ChannelGridDTO> GetById(int id, bool isActive = true);
        public Task<List<ChannelGridDTO>> ListByServer(int channelId, bool isActive = true);
        Task<ChannelDTO> AddEdit(ChannelDTO channel);
        Task<ChannelDTO> DeleteById(int id);

    }
}
