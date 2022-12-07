using disclone_api.DTOs.ChannelDTOs;

namespace disclone_api.Services.ChannelServices
{
    public interface IChannelService
    {
        public Task<ChannelDTO> GetByIdAsync(int id, bool isActive = true);
        public Task<List<ChannelDTO>> ListByServer(int channelId, bool isActive = true);
        Task<ChannelDTO> AddEditAsync(ChannelDTO channel);
        Task<ChannelDTO> ToggleInactiveById(int id);

    }
}
