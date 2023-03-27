using disclone_api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace disclone_api.Services
{
    public interface IMessageService : IMainService<MessageDTO, MessageDetailDTO>
    {
        Task<List<MessageDetailDTO>> ListByChannelId(int channelId, bool isActive = true);
        Task<List<MessageDetailDTO>> ListByUserId(int userId, bool isActive = true);

        Task<List<MessageDTO>> FilterByContent([FromQuery] string Content, [FromQuery] string orderby = "CreationDate");
    }
}
