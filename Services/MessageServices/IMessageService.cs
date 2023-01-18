﻿using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IMessageService
    {
        Task<MessageGridDTO> GetById(int id, bool isActive = true);
        Task<List<MessageGridDTO>> ListByChannelId(int channelId, bool isActive = true);
        Task<List<MessageGridDTO>> ListByUserId(int userId, bool isActive = true);
        Task<MessageDTO> Add(MessageDTO message);
        Task<MessageDTO> EditById(MessageDTO message);
        Task<MessageDTO> DeleteById(int id);
    }
}
