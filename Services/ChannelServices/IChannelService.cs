using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IChannelService : IMainService<ChannelDTO>
    {
        public Task<List<ChannelDTO>> ListByServer(int channelId, bool isActive = true);

    }
}
