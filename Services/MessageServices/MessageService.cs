using AutoMapper;
using disclone_api.DTO;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;
using disclone_api.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

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
            await _hubContext.Clients.All.SendAsync("channel_" + message.ChannelId, "MESSAGE_SENT", message.Id);
            return _mapper.Map<MessageDTO>(result);
        }
        public async Task<MessageDTO> EditById(MessageDTO message)
        {
            var oldMessage = await _context.Message.FirstOrDefaultAsync(x => x.Id.Equals(message.Id) && x.IsActive == true);
            message.CreationDate = oldMessage.CreationDate;
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
        
        public async Task<List<MessageDTO>> FilterByContent([FromQuery] string Content = "", [FromQuery] string orderby = "CreationDate")
        {
            IQueryable<Message> query = null;
            if(!string.IsNullOrEmpty(Content)){
                query = _context.Message.Where(x => x.Content.ToLower().Contains(Content.ToLower()));
            } else {
                query = _context.Message;
            }
            

            switch (orderby.ToLower())
            {
                case "creationdate":
                    query = query.OrderByDescending(x => x.CreationDate);
                    break;
                case "id":
                    query = query.OrderBy(x => x.Id);
                    break;
                case "content":
                    query = query.OrderBy(x => x.Content);
                    break;
                case "userid":
                    query = query.OrderBy(x => x.UserId);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreationDate);
                    break;
            }

            return _mapper.Map<List<MessageDTO>>(await query.ToListAsync());
        }

        public async Task<List<MessageDetailDTO>> FilterByChannelContent(int channelId, [FromQuery] string Content = "", [FromQuery] string orderby = "CreationDate")
        {
            IQueryable<Message> query = null;
            
            if(!string.IsNullOrEmpty(Content)){
                query = _context.Message.Where(x => x.Content.ToLower().Contains(Content.ToLower()) && x.ChannelId == channelId && x.IsActive == true).Include(x => x.User);
            } else {
                query = _context.Message.Where(x => x.IsActive == true && x.ChannelId == channelId).Include(x => x.User);
            }
            
            switch (orderby.ToLower())
            {
                case "creationdate":
                    query = query.OrderBy(x => x.CreationDate);
                    break;
                case "id":
                    query = query.OrderBy(x => x.Id);
                    break;
                case "content":
                    query = query.OrderBy(x => x.Content);
                    break;
                case "userid":
                    query = query.OrderBy(x => x.UserId);
                    break;
                default:
                    query = query.OrderBy(x => x.CreationDate);
                    break;
            }

            return _mapper.Map<List<MessageDetailDTO>>(await query.ToListAsync());
        }

        public async Task<List<MessageDetailDTO>> ListByChannelId(int channelId, bool isActive)
        {
            return _mapper.Map<List<MessageDetailDTO>>(await _context.Message
                .Where(x => x.ChannelId.Equals(channelId) && x.IsActive == isActive).OrderBy(x => x.CreationDate)
                .Include(x => x.User)
                .ToListAsync());
        }

        public async Task<List<MessageDetailDTO>> ListByUserId(int userId, bool isActive)
        {
            return _mapper.Map<List<MessageDetailDTO>>(await _context.Message
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
            await _hubContext.Clients.All.SendAsync("channel_" + message.ChannelId, "MESSAGE_DELETED", message.Id);
            await _context.SaveChangesAsync();
            return _mapper.Map<MessageDTO>(message);
        } 
        #endregion
    }
}
