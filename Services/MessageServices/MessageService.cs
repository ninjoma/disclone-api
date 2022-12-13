using AutoMapper;
using disclone_api.DTOs.MessageDTOs;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Set
        public async Task<MessageDTO> AddEditAsync(MessageDTO message)
        {
            if (message.Id != 0)
            {
                return await UpdateMessageAsync(message);
            }
            else
            {
                return await CreateMessageAsync(message);
            }
        }

        public async Task<MessageDTO> CreateMessageAsync(MessageDTO message)
        {
            message.CreationDate = DateTime.UtcNow;
            var result = _mapper.Map<Message>(message);
            await _context.Message.AddAsync(result);
            await _context.SaveChangesAsync();
            return _mapper.Map<MessageDTO>(result);
        }
        public async Task<MessageDTO> UpdateMessageAsync(MessageDTO message)
        {
            var oldMessage = await _context.Message.FirstOrDefaultAsync(x => x.Id.Equals(message.Id) && x.IsActive == true);
            _mapper.Map<MessageDTO, Message>(message, oldMessage);
            await _context.SaveChangesAsync();
            return _mapper.Map<MessageDTO>(oldMessage);
        }
        #endregion

        #region Get
        public async Task<MessageGridDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<MessageGridDTO>(await _context.Message
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<List<MessageGridDTO>> ListByChannelId(int channelId, bool isActive)
        {
            return _mapper.Map<List<MessageGridDTO>>(await _context.Message
                .Where(x => x.ChannelId.Equals(channelId) && x.IsActive == isActive)
                .Include(x => x.User).OrderBy(x => x.CreationDate)
                .ToListAsync());
        }

        public async Task<List<MessageGridDTO>> ListByUserId(int userId, bool isActive)
        {
            return _mapper.Map<List<MessageGridDTO>>(await _context.Message
                .Where(x => x.UserId.Equals(userId) && isActive == true)
                .Include(x => x.User)
                .ToListAsync());
        }
        #endregion
        

        #region Delete
        public async Task<MessageDTO> ToggleInactiveById(int id)
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
