﻿using AutoMapper;
using disclone_api.DTO;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services
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
        public async Task<ServerDTO> Add(ServerDTO server)
        {
            var newServer = _mapper.Map<Server>(server);
            await _context.Server.AddAsync(newServer);
            await _context.SaveChangesAsync();
            return _mapper.Map<ServerDTO>(newServer);
        }

        public async Task<ServerDTO> EditById(ServerDTO server)
        {
            var oldServer = await _context.Server.FirstOrDefaultAsync(x => x.Id.Equals(server.Id));
            _mapper.Map<ServerDTO, Server>(server, oldServer);
            await _context.SaveChangesAsync();
            return _mapper.Map<ServerDTO>(oldServer);
        }
        #endregion

        #region Get
        public async Task<ServerDetailDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<ServerDetailDTO>(await _context.Server.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<List<ServerDTO>> ListByName(string name, bool isActive = true)
        {
            return _mapper.Map<List<ServerDTO>>(await _context.Server.Where(x => x.Name.Contains(name) && x.IsActive == isActive).ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<ServerDTO> DeleteById(int id)
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
