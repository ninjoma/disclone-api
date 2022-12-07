using disclone_api.DTOs.ChannelDTOs;

namespace disclone_api.Services.ChannelServices
{
    public interface IChannelService
    {
        public Task<ChannelDTO> GetByIdAsync(int id);
        public Task<List<ChannelDTO>> ListByServer(int channelId);
        Task<ChannelDTO> AddEditAsync(ChannelDTO channel);
        Task<ChannelDTO> ToggleInactiveById(int id);

    }
}
