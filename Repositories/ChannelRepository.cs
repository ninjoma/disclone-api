using AutoMapper;
using disclone_api.DTO;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Repositories
{
    public class ChannelRepository : IMainRepository<ChannelDTO>
    {

        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ChannelRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Disposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Metodos
        public async Task<ChannelDTO> Add(ChannelDTO newDto)
        {
            var newChannel = _mapper.Map<Channel>(newDto);
            await _context.AddAsync(newChannel);
            await this.Save();
            return _mapper.Map<ChannelDTO>(newChannel);
        }

        public async Task<bool> Delete(int id)
        {
            var channel = await _context.Channel.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (channel != null)
            {
                if (channel.IsActive)
                {
                    channel.IsActive = false;
                }
                else
                {
                    channel.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ChannelDTO> Edit(ChannelDTO newDto)
        {
            var oldChannel = await _context.Channel.FirstOrDefaultAsync(x => x.Id.Equals(newDto.Id));
            _mapper.Map<ChannelDTO, Channel>(newDto, oldChannel);
            await _context.SaveChangesAsync();
            return _mapper.Map<ChannelDTO>(oldChannel);
        }

        public async Task<ChannelDTO> GetById(int id)
        {
            return _mapper.Map<ChannelDTO>(await _context.Channel
                .Include(x => x.Server)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == true));
        }

        public async Task<IEnumerable<ChannelDTO>> List()
        {
            return _mapper.Map<IEnumerable<ChannelDTO>>(await _context.Channel.Where(x => x.IsActive == true).ToListAsync());
        }

        public async Task<bool> Save()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}