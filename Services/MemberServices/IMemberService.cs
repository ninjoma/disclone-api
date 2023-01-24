﻿using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IMemberService : IMainService<MemberDTO>
    {
        Task<List<MemberGridDTO>> ListByUserId(int id, bool isActive = true);
        Task<MemberGridDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<MemberGridDTO>> ListByServerId(int id, bool isActive = true);
    }
}
