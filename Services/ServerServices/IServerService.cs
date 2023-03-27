﻿
using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IServerService : IMainService<ServerDTO,ServerDetailDTO>
    {
        Task<List<ServerDetailDTO>> ListByName(string name, bool isActive = true);
    }
}
