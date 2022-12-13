using AutoMapper;
using disclone_api.DTOs.ServerDTOs;
using disclone_api.Services.MemberServices;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.ServerServices
{
    public class ServerService : IServerService
    {
        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly IMemberService _memberSv;

        public ServerService(DataContext context, IMapper mapper, IMemberService memberSv)
        {
            _context = context;
            _mapper = mapper;
            _memberSv = memberSv;
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
            var newServer = _mapper.Map<Server>(server);
            await _context.Server.AddAsync(newServer);
            await _context.SaveChangesAsync();
            return _mapper.Map<ServerDTO>(newServer);
        }

        public async Task<ServerDTO> UpdateServerAsync(ServerDTO server)
        {
            var oldServer = await _context.Server.FirstOrDefaultAsync(x => x.Id.Equals(server.Id));
            _mapper.Map<ServerDTO, Server>(server, oldServer);
            await _context.SaveChangesAsync();
            return _mapper.Map<ServerDTO>(oldServer);
        }
        #endregion

        #region Get
        public async Task<ServerGridDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<ServerGridDTO>(await _context.Server.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<List<ServerGridDTO>> ListByName(string name, bool isActive = true)
        {
            return _mapper.Map<List<ServerGridDTO>>(await _context.Server.Where(x => x.Name.Contains(name) && x.IsActive == isActive).ToListAsync());
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
