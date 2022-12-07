using disclone_api.DTOs.MessageDTOs;

namespace disclone_api.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        public Task<MessageDTO> AddEditAsync(MessageDTO message)
        {
            throw new NotImplementedException();
        }
        public Task<MessageDTO> CreateMessageAsync(MessageDTO message)
        {
            throw new NotImplementedException();
        }
        public Task<MessageDTO> UpdateMessageAsync(MessageDTO message)
        {
            throw new NotImplementedException();
        }

        public Task<MessageDTO> GetById(int id, bool isActive)
        {
            throw new NotImplementedException();
        }

        public Task<List<MessageDTO>> ListByChannelId(int channelId, bool isActive)
        {
            throw new NotImplementedException();
        }

        public Task<List<MessageDTO>> ListByUserId(int userId, bool isActive)
        {
            throw new NotImplementedException();
        }

        public Task<MessageDTO> ToggleInactiveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
