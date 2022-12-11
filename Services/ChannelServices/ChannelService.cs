using AutoMapper;
using disclone_api.DTOs.ChannelDTOs;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.ChannelServices
{
    public class ChannelService : IChannelService
    {

        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ChannelService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Set
        public async Task<ChannelDTO> AddEditAsync(ChannelDTO channel)
        {
            if (channel.Id != 0)
            {
                return await UpdateChannelAsync(channel);
            }
            else
            {
                return await CreateChannelAsync(channel);
            }
        }
        public async Task<ChannelDTO> CreateChannelAsync(ChannelDTO channel)
        {
            var newChannel = _mapper.Map<Channel>(channel);
            await _context.Channel.AddAsync(newChannel);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChannelDTO>(newChannel);
        }
        public async Task<ChannelDTO> UpdateChannelAsync(ChannelDTO channel)
        {
            var oldChannel = await _context.Channel.FirstOrDefaultAsync(x => x.Id.Equals(channel.Id));
            _mapper.Map<ChannelDTO, Channel>(channel, oldChannel);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChannelDTO>(oldChannel);
        }
        #endregion

        #region Get
        public async Task<ChannelGridDTO> GetByIdAsync(int id, bool isActive = true)
        {
            return _mapper.Map<ChannelGridDTO>(await _context.Channel
                .Include(x => x.Server)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<List<ChannelGridDTO>> ListByServer(int channelId, bool isActive = true)
        {
            return _mapper.Map<List<ChannelGridDTO>>(await _context.Channel
                .Include(x => x.Server)
                .Include(x => x.Messages)
                .Where(x => x.ServerId.Equals(channelId) && x.IsActive == isActive)
                .ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<ChannelDTO> ToggleInactiveById(int id)
        {
            var channel = await _context.Channel.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (channel.IsActive)
            {
                channel.IsActive = false;
            }
            else
            {
                channel.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ChannelDTO>(channel);
        } 
        #endregion
    }
}
