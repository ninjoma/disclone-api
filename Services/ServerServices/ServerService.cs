using AutoMapper;
using disclone_api.DTOs.ServerDTOs;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.ServerServices
{
    public class ServerService : IServerService
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ServerService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Set
        public async Task<ServerDTO> AddEditAsync(ServerDTO server)
        {
            if (server.Id != 0)
            {
                return await UpdateServerAsync(server);
            }
            else
            {
                return await CreateServerAsync(server);
            }
        }
        public async Task<ServerDTO> CreateServerAsync(ServerDTO server)
        {
            await _context.Server.AddAsync(_mapper.Map<Server>(server));
            await _context.SaveChangesAsync();
            return server;
        }

        public async Task<ServerDTO> UpdateServerAsync(ServerDTO server)
        {
            var oldServer = await _context.Server.FirstOrDefaultAsync(x => x.Id.Equals(server.Id));
            oldServer = _mapper.Map<Server>(server);
            await _context.SaveChangesAsync();
            return _mapper.Map<ServerDTO>(oldServer);
        }
        #endregion

        #region Get
        public async Task<ServerDTO> GetById(int id)
        {
            return _mapper.Map<ServerDTO>(await _context.Server.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == true));
        }

        public async Task<List<ServerDTO>> ListByName(string name)
        {
            return _mapper.Map<List<ServerDTO>>(await _context.Server.Where(x => x.Name.Contains(name) && x.IsActive == true).ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<ServerDTO> ToggleInactiveById(int id)
        {
            var server = await _context.Server.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (server.IsActive)
            {
                server.IsActive = false;
            }
            else
            {
                server.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ServerDTO>(server);
        } 
        #endregion
    }
}
