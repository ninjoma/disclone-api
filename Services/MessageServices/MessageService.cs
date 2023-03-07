using AutoMapper;
using disclone_api.DTO;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;
using disclone_api.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace disclone_api.Services
{
    public class MessageService : IMessageService
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly IHubContext<EventHub> _hubContext;
        public MessageService(DataContext context, IMapper mapper, IHubContext<EventHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        #endregion

        #region Set

        public async Task<MessageDTO> Add(MessageDTO message)
        {
            message.CreationDate = DateTime.UtcNow;
            var result = _mapper.Map<Message>(message);
            await _context.Message.AddAsync(result);
            await _context.SaveChangesAsync();
            
            var response = new {
                channelId = message.ChannelId
            };
            await _hubContext.Clients.All.SendAsync("onEvent", "MESSAGE_SENT", JsonSerializer.Serialize(response));
            
            return _mapper.Map<MessageDTO>(result);
        }
        public async Task<MessageDTO> EditById(MessageDTO message)
        {
            var oldMessage = await _context.Message.FirstOrDefaultAsync(x => x.Id.Equals(message.Id) && x.IsActive == true);
            _mapper.Map<MessageDTO, Message>(message, oldMessage);
            await _context.SaveChangesAsync();
            return _mapper.Map<MessageDTO>(oldMessage);
        }
        #endregion

        #region Get
        public async Task<MessageDetailDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<MessageDetailDTO>(await _context.Message
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<List<MessageDTO>> ListByChannelId(int channelId, bool isActive)
        {
            return _mapper.Map<List<MessageDTO>>(await _context.Message
                .Where(x => x.ChannelId.Equals(channelId) && x.IsActive == isActive)
                .Include(x => x.User).OrderBy(x => x.CreationDate)
                .ToListAsync());
        }

        public async Task<List<MessageDTO>> ListByUserId(int userId, bool isActive)
        {
            return _mapper.Map<List<MessageDTO>>(await _context.Message
                .Where(x => x.UserId.Equals(userId) && isActive == true)
                .Include(x => x.User)
                .ToListAsync());
        }
        #endregion
        

        #region Delete
        public async Task<MessageDTO> DeleteById(int id)
        {
            var message = await _context.Message.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (message.IsActive)
            {
                message.IsActive = false;
            }
            else
            {
                message.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<MessageDTO>(message);
        } 
        #endregion
    }
}
