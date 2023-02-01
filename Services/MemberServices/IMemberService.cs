using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IMemberService : IMainService<MemberDTO, MemberDetailDTO>
    {
        Task<List<MemberDTO>> ListByUserId(int id, bool isActive = true);
        Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<MemberDTO>> ListByServerId(int id, bool isActive = true);
    }
}
