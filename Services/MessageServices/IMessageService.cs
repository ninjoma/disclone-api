using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IMessageService : IMainService<MessageDTO>
    {
        Task<List<MessageGridDTO>> ListByChannelId(int channelId, bool isActive = true);
        Task<List<MessageGridDTO>> ListByUserId(int userId, bool isActive = true);
    }
}
