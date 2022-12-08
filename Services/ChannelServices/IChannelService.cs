using disclone_api.DTOs.ChannelDTOs;

namespace disclone_api.Services.ChannelServices
{
    public interface IChannelService
    {
        public Task<ChannelGridDTO> GetByIdAsync(int id, bool isActive = true);
        public Task<List<ChannelGridDTO>> ListByServer(int channelId, bool isActive = true);
        Task<ChannelDTO> AddEditAsync(ChannelDTO channel);
        Task<ChannelDTO> ToggleInactiveById(int id);

    }
}
