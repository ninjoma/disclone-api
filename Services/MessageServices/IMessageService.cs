using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IMessageService : IMainService<MessageDTO, MessageDetailDTO>
    {
        Task<List<MessageDTO>> ListByChannelId(int channelId, bool isActive = true);
        Task<List<MessageDTO>> ListByUserId(int userId, bool isActive = true);
    }
}
