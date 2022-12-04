using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.MemberServices
{
    public interface IMemberService
    {
        Task<MemberDTO> GetById(int id);
        Task<List<MemberDTO>> ListByUserId(int id);
        Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId);
        Task<List<MemberDTO>> ListByServerId(int id);
        Task<MemberDTO> AddEditAsync(MemberDTO member);
        Task<MemberDTO> ToggleInactiveById(int id);
    }
}
