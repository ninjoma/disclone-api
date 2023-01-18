using disclone_api.DTOs.ChannelDTOs;

namespace disclone_api.Services.ChannelServices
{
    public interface IChannelService
    {
        public Task<ChannelGridDTO> GetById(int id, bool isActive = true);
        public Task<List<ChannelGridDTO>> ListByServer(int channelId, bool isActive = true);
        Task<ChannelDTO> EditById(ChannelDTO channel);
        Task<ChannelDTO> Add(ChannelDTO channel);
        Task<ChannelDTO> DeleteById(int id);

    }
}
