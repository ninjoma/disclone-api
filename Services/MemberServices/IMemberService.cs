using disclone_api.DTOs.MemberDTOs;

namespace disclone_api.Services.MemberServices
{
    public interface IMemberService
    {
        Task<MemberDTO> GetById(int id);
        Task<MemberDTO> ListByUserId(int id);
        Task<MemberDTO> GetByServerIdAndByUserId(int userId, int serverId);
        Task<MemberDTO> ListByServerId(int id);
        Task<MemberDTO> AddEditAsync(MemberDTO server);
        Task<MemberDTO> ToggleInactiveById(int id);
    }
}
