using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.MemberServices
{
    public interface IMemberService
    {
        Task<MemberDTO> GetById(int id, bool isActive = true);
        Task<List<MemberDTO>> ListByUserId(int id, bool isActive = true);
        Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId, bool isActive = true);
        Task<List<MemberDTO>> ListByServerId(int id, bool isActive = true);
        Task<MemberDTO> AddEditAsync(MemberDTO member);
        Task<MemberDTO> ToggleInactiveById(int id);
    }
}
