using disclone_api.DTO;

namespace disclone_api.Services
{
    public interface IMemberService : IMainService<MemberDTO, MemberDetailDTO>
    {
        Task<List<MemberDetailDTO>> ListByUserId(int id, bool isActive = true);
        Task<MemberDetailDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<MemberDetailDTO>> ListByServerId(int id, bool isActive = true);
    }
}
