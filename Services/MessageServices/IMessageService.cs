using disclone_api.DTOs.MessageDTOs;

namespace disclone_api.Services.MessageServices
{
    public interface IMessageService
    {
        Task<MessageDTO> GetById(int id, bool isActive);
        Task<List<MessageDTO>> ListByChannelId(int channelId, bool isActive);
        Task<List<MessageDTO>> ListByUserId(int userId, bool isActive);
        Task<MessageDTO> AddEditAsync(MessageDTO message);
        Task<MessageDTO> ToggleInactiveById(int id);
    }
}
